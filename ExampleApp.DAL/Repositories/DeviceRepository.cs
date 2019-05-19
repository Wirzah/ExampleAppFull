using ExampleApp.DAL.Core;
using ExampleApp.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExampleApp.DAL.Repositories
{
  public interface IDeviceRepository : IRepositoryBase<Device>
  {

  }

  public class DeviceRepository : RepositoryBase<Device>, IDeviceRepository
  {
    public DeviceRepository(ExampleAppDbContext dbContext)
      : base(dbContext) { }
  }
}
