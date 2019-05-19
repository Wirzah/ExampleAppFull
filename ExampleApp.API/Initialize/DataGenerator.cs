using ExampleApp.DAL;
using ExampleApp.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExampleApp.API.Initialize
{
  public class DataGenerator
  {
    public static void Initialize(IServiceProvider serviceProvider)
    {
      using (var context = new ExampleAppDbContext(serviceProvider.GetRequiredService<DbContextOptions<ExampleAppDbContext>>()))
      {
        var racunar = new DeviceType
        {
          Name = "Racunar"
        };
        var laptop = new DeviceType
        {
          Name = "Laptop",
          ParentDeviceType = racunar
        };
        context.DeviceTypes.AddRange(racunar, laptop);

        var ramMemorija = new DeviceTypeProperty
        {
          DeviceType = racunar,
          Name = "RAM memorija"
        };
        var operativniSistem = new DeviceTypeProperty
        {
          DeviceType = racunar,
          Name = "Operativni sistem"
        };
        var procesor = new DeviceTypeProperty
        {
          DeviceType = racunar,
          Name = "Procesor"
        };

        var dijagonala = new DeviceTypeProperty
        {
          DeviceType = laptop,
          Name = "Dijagonala"
        };

        context.DeviceTypeProperties.AddRange(ramMemorija, operativniSistem, procesor, dijagonala);

        var mirzinRacunar = new Device
        {
          DeviceType = racunar,
          Name = "Mirzin racunar"
        };
        var mirzinLaptop = new Device
        {
          DeviceType = laptop,
          Name = "Mirzin laptop"
        };

        context.Devices.AddRange(mirzinRacunar, mirzinLaptop);

        var racunarRam = new DevicePropertyValue
        {
          Device = mirzinRacunar,
          DeviceTypeProperty = ramMemorija,
          Value = "16 GB"
        };
        var racunarOS = new DevicePropertyValue
        {
          Device = mirzinRacunar,
          DeviceTypeProperty = operativniSistem,
          Value = "Windows 10"
        };
        var racunarProcesor = new DevicePropertyValue
        {
          Device = mirzinRacunar,
          DeviceTypeProperty = procesor,
          Value = "Intel Core i7 7993"
        };

        var laptopRam = new DevicePropertyValue
        {
          Device = mirzinLaptop,
          DeviceTypeProperty = ramMemorija,
          Value = "8 GB"
        };
        var laptopOS = new DevicePropertyValue
        {
          Device = mirzinLaptop,
          DeviceTypeProperty = operativniSistem,
          Value = "Ubuntu Linux"
        };
        var laptopProcesor = new DevicePropertyValue
        {
          Device = mirzinLaptop,
          DeviceTypeProperty = procesor,
          Value = "Intel Core i3 4550"
        };
        var laptopDijagonala = new DevicePropertyValue
        {
          Device = mirzinLaptop,
          DeviceTypeProperty = dijagonala,
          Value = "15.4 inch"
        };

        context.DevicePropertyValues.AddRange(racunarRam, racunarOS, racunarProcesor, laptopRam, laptopOS, laptopProcesor, laptopDijagonala);

        context.SaveChanges();
      }
    }
  }
}
