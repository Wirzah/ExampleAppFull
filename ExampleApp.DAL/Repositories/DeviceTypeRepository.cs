using ExampleApp.DAL.Core;
using ExampleApp.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExampleApp.DAL.Repositories
{
  public interface IDeviceTypeRepository : IRepositoryBase<DeviceType>
  {

  }

  public class DeviceTypeRepository : RepositoryBase<DeviceType>, IDeviceTypeRepository
  {
    public DeviceTypeRepository(ExampleAppDbContext dbContext)
      : base(dbContext) { }
  }
}
