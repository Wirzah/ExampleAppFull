using ExampleApp.DAL.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ExampleApp.DAL.Models
{
  public class DeviceType : IEntity
  {
    public DeviceType()
    {
      Devices = new HashSet<Device>();
      DeviceTypeProperties = new HashSet<DeviceTypeProperty>();
      ChildrenDeviceTypes = new HashSet<DeviceType>();
    }

    public int Id { get; set; }
    [Required]
    [StringLength(255)]
    public string Name { get; set; }
    public int? ParentId { get; set; }
    public ICollection<Device> Devices { get; set; }
    public ICollection<DeviceTypeProperty> DeviceTypeProperties { get; set; }
    public ICollection<DeviceType> ChildrenDeviceTypes { get; set; }
    public DeviceType ParentDeviceType { get; set; }
  }
}
