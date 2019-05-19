using ExampleApp.DAL.Models;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExampleApp.DAL
{
  public class ExampleAppDbContext : DbContext
  {
    public ExampleAppDbContext(DbContextOptions options)
      : base(options)
    {
    }

    public DbSet<DevicePropertyValue> DevicePropertyValues { get; set; }
    public DbSet<Device> Devices { get; set; }
    public DbSet<DeviceTypeProperty> DeviceTypeProperties { get; set; }
    public DbSet<DeviceType> DeviceTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Device>()
          .HasMany(e => e.DevicePropertyValues)
          .WithOne(e => e.Device)
          .OnDelete(DeleteBehavior.Cascade);

      modelBuilder.Entity<DeviceTypeProperty>()
          .HasMany(e => e.DevicePropertyValues)
          .WithOne(e => e.DeviceTypeProperty)
          .OnDelete(DeleteBehavior.Cascade);

      modelBuilder.Entity<DeviceType>()
          .HasMany(e => e.Devices)
          .WithOne(e => e.DeviceType)
          .OnDelete(DeleteBehavior.Cascade);

      modelBuilder.Entity<DeviceType>()
          .HasMany(e => e.DeviceTypeProperties)
          .WithOne(e => e.DeviceType)
          .OnDelete(DeleteBehavior.Cascade);

      modelBuilder.Entity<DeviceType>()
          .HasMany(e => e.ChildrenDeviceTypes)
          .WithOne(e => e.ParentDeviceType)
          .HasForeignKey(e => e.ParentId)
          .OnDelete(DeleteBehavior.Cascade);
    }
  }
}
