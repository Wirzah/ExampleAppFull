using ExampleApp.DAL.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ExampleApp.DAL.Models
{
  public class DeviceTypeProperty : IEntity
  {
    public DeviceTypeProperty()
    {
      DevicePropertyValues = new HashSet<DevicePropertyValue>();
    }

    public int Id { get; set; }
    [Required]
    [StringLength(255)]
    public string Name { get; set; }
    public int DeviceTypeId { get; set; }
    public ICollection<DevicePropertyValue> DevicePropertyValues { get; set; }
    public DeviceType DeviceType { get; set; }
  }
}
