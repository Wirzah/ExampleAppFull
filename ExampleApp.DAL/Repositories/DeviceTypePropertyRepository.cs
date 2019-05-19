using ExampleApp.DAL.Core;
using ExampleApp.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExampleApp.DAL.Repositories
{
  public interface IDeviceTypePropertyRepository : IRepositoryBase<DeviceTypeProperty>
  {

  }

  public class DeviceTypePropertyRepository : RepositoryBase<DeviceTypeProperty>, IDeviceTypePropertyRepository
  {
    public DeviceTypePropertyRepository(ExampleAppDbContext dbContext)
      : base(dbContext) { }
  }
}
