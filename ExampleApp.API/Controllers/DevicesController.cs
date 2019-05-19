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
  public class DevicesController : ControllerBase
  {
    private readonly UnitOfWork _unitOfWork;

    public DevicesController(UnitOfWork unitOfWork)
    {
      _unitOfWork = unitOfWork;
    }

    // GET: api/Devices
    [HttpGet]
    [EnableQuery]
    public async Task<ActionResult<IEnumerable<Device>>> GetDevices()
    {
      var result = await _unitOfWork.DeviceRepository.GetAsync(includeProperties: "DeviceType,DevicePropertyValues");
      return Ok(result);
    }

    // GET: api/Devices/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Device>> GetDevice(int id)
    {
      var device = await _unitOfWork.DeviceRepository.GetByIdAsync(id, "DeviceType,DevicePropertyValues");

      if (device == null)
      {
        return NotFound();
      }

      return device;
    }

    // PUT: api/Devices/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutDevice(int id, Device device)
    {
      if (id != device.Id)
      {
        return BadRequest();
      }

      try
      {
        await _unitOfWork.SaveAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!DeviceExists(id))
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

    // POST: api/Devices
    [HttpPost]
    public async Task<ActionResult<Device>> PostDevice([FromBody]Device device)
    {
      _unitOfWork.DeviceRepository.Insert(device);
      await _unitOfWork.SaveAsync();

      return CreatedAtAction("GetDevice", new { id = device.Id }, device);
    }

    // DELETE: api/Devices/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<Device>> DeleteDevice(int id)
    {
      var device = await _unitOfWork.DeviceRepository.GetByIdAsync(id);
      if (device == null)
      {
        return NotFound();
      }

      _unitOfWork.DeviceRepository.Delete(device);
      await _unitOfWork.SaveAsync();

      return device;
    }

    private bool DeviceExists(int id)
    {
      return _unitOfWork.DeviceRepository.Get().Any(e => e.Id == id);
    }

  }
}
