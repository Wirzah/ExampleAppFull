using ExampleApp.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExampleApp.DAL.Core
{
  public class UnitOfWork : IDisposable
  {
    private readonly ExampleAppDbContext _dbContext;
    private DevicePropertyValueRepository _devicePropertyValueRepository;
    private DeviceRepository _deviceRepository;
    private DeviceTypePropertyRepository _deviceTypePropertyRepository;
    private DeviceTypeRepository _deviceTypeRepository;

    public UnitOfWork(ExampleAppDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public void Save() => _dbContext.SaveChanges();

    public async Task SaveAsync() => await _dbContext.SaveChangesAsync();

    public DevicePropertyValueRepository DevicePropertyValueRepository
    {
      get
      {
        if (_devicePropertyValueRepository == null)
          _devicePropertyValueRepository = new DevicePropertyValueRepository(_dbContext);
        return _devicePropertyValueRepository;
      }
    }

    public DeviceRepository DeviceRepository
    {
      get
      {
        if (_deviceRepository == null)
          _deviceRepository = new DeviceRepository(_dbContext);
        return _deviceRepository;
      }
    }

    public DeviceTypePropertyRepository DeviceTypePropertyRepository
    {
      get
      {
        if (_deviceTypePropertyRepository == null)
          _deviceTypePropertyRepository = new DeviceTypePropertyRepository(_dbContext);
        return _deviceTypePropertyRepository;
      }
    }

    public DeviceTypeRepository DeviceTypeRepository
    {
      get
      {
        if (_deviceTypeRepository == null)
          _deviceTypeRepository = new DeviceTypeRepository(_dbContext);
        return _deviceTypeRepository;
      }
    }

    #region IDisposable Support
    private bool disposedValue = false; // To detect redundant calls

    protected virtual void Dispose(bool disposing)
    {
      if (!disposedValue)
      {
        if (disposing)
        {
          // TODO: dispose managed state (managed objects).
        }

        // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
        // TODO: set large fields to null.

        disposedValue = true;
      }
    }

    // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
    // ~UnitOfWork()
    // {
    //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
    //   Dispose(false);
    // }

    // This code added to correctly implement the disposable pattern.
    public void Dispose()
    {
      // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
      Dispose(true);
      // TODO: uncomment the following line if the finalizer is overridden above.
      // GC.SuppressFinalize(this);
    }
    #endregion
  }
}
