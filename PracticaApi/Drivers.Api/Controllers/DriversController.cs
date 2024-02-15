using Drivers.Api.Models;
using Drivers.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Drivers.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DriversController : ControllerBase
{
   
    private readonly ILogger<DriversController> _logger;
    private readonly DriverServices _driverServices;

    public DriversController(
        ILogger<DriversController> logger,
         DriverServices driverServices)
    {
        _logger = logger;
        _driverServices= driverServices;
    }
    [HttpGet]
    public async Task<IActionResult> GetDrivers()
    {
        var driver = await _driverServices.GetAsync();
        return Ok(driver);
    }

     [HttpGet("{id}")]
      public async Task<IActionResult> GetDriversById(string id)
    {
        return Ok(await _driverServices.GetDriverId(id));
    }




     [HttpPost]
   public async Task<IActionResult> CreateDriver([FromBody] Drive drive)
   {
    if(drive==null)
    return BadRequest();
    if(drive.Name == string.Empty)
    ModelState.AddModelError("Name","El driver no debe estar vacio");
    await _driverServices.InsertarDriver(drive);
    return Created("created", true);
   }
   [HttpPut("{id}")]

 public async Task<IActionResult> UpdateDriver([FromBody] Drive drive, string id)
   {
    if(drive==null)
    return BadRequest();
    if(drive.Name == string.Empty)
    ModelState.AddModelError("Name","El driver no debe estar vacio");

   drive.Id=id;
    await _driverServices.InsertarDriver(drive);
    return Created("created", true);
   }
   [HttpDelete("{id}")]

   public async Task<IActionResult> DeleteDriver(string id)
   {
    await _driverServices.DeleteDriver(id);
    return NoContent();
   }

}
