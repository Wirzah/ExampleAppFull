using ExampleApp.DAL.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ExampleApp.DAL.Models
{
  public class DevicePropertyValue : IEntity
  {
    public int Id { get; set; }
    [Required]
    public string Value { get; set; }
    public int DeviceTypePropertyId { get; set; }
    public int DeviceId { get; set; }
    public Device Device { get; set; }
    public DeviceTypeProperty DeviceTypeProperty { get; set; }
  }
}
