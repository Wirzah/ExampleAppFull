using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExampleApp.DAL;
using ExampleApp.DAL.Models;
using ExampleApp.DAL.Core;
using Microsoft.AspNet.OData;

namespace ExampleApp.API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class DeviceTypesController : ControllerBase
  {
    private readonly UnitOfWork _unitOfWork;

    public DeviceTypesController(UnitOfWork unitOfWork)
    {
      _unitOfWork = unitOfWork;
    }

    // GET: api/DeviceTypes
    [HttpGet]
    [EnableQuery]
    public async Task<ActionResult<IEnumerable<DeviceType>>> GetDeviceTypes()
    {
      var result = await _unitOfWork.DeviceTypeRepository.GetAsync();
      return Ok(result);
    }

    // GET: api/DeviceTypes/5
    [HttpGet("{id}")]
    public async Task<ActionResult<DeviceType>> GetDeviceType(int id)
    {
      var deviceType = await _unitOfWork.DeviceTypeRepository.GetByIdAsync(id);

      if (deviceType == null)
      {
        return NotFound();
      }

      return deviceType;
    }

    // PUT: api/DeviceTypes/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutDeviceType(int id, DeviceType deviceType)
    {
      if (id != deviceType.Id)
      {
        return BadRequest();
      }

      try
      {
        await _unitOfWork.SaveAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!DeviceTypeExists(id))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }

      return NoContent();
    }

    // POST: api/DeviceTypes
    [HttpPost]
    public async Task<ActionResult<DeviceType>> PostDeviceType(DeviceType deviceType)
    {
      _unitOfWork.DeviceTypeRepository.Insert(deviceType);
      await _unitOfWork.SaveAsync();

      return CreatedAtAction("GetDeviceType", new { id = deviceType.Id }, deviceType);
    }

    // DELETE: api/DeviceTypes/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<DeviceType>> DeleteDeviceType(int id)
    {
      var deviceType = await _unitOfWork.DeviceTypeRepository.GetByIdAsync(id);
      if (deviceType == null)
      {
        return NotFound();
      }

      _unitOfWork.DeviceTypeRepository.Delete(deviceType);
      await _unitOfWork.SaveAsync();

      return deviceType;
    }

    private bool DeviceTypeExists(int id)
    {
      return _unitOfWork.DeviceTypeRepository.Get().Any(e => e.Id == id);
    }
  }
}
