using ExampleApp.DAL.Core;
using ExampleApp.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExampleApp.DAL.Repositories
{
  public interface IDevicePropertyValueRepository : IRepositoryBase<DevicePropertyValue>
  {

  }

  public class DevicePropertyValueRepository : RepositoryBase<DevicePropertyValue>, IDevicePropertyValueRepository
  {
    public DevicePropertyValueRepository(ExampleAppDbContext dbContext)
      : base(dbContext) { }
  }
}
