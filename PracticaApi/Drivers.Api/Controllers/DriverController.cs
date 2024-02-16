using Microsoft.AspNetCore.Mvc;
using Drivers.Api.Models;

namespace Drivers.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DriverController : ControllerBase
{
    private readonly ILogger<DriverController> _logger;

    private readonly DriverServices.DriverServices _driverServices;

    public DriverController(ILogger<DriverController> logger, DriverServices.DriverServices driverServices)
    {
        _logger = logger;
        _driverServices = driverServices;
    }

    ///Obtener todos los Drivers
    [HttpGet]
    public async Task<IActionResult> GetDrivers()
    {
        var drivers = await _driverServices.GetAsync();
        return Ok(drivers);
    }

    ///Obtener Driver por Id
    [HttpGet("{Id}")]
    public async Task<IActionResult> GetDriversById(string Id)
    {
        return Ok(await _driverServices.GetDriverById(Id));
    }

    ///Crear Driver
    [HttpPost]
    public async Task<IActionResult> CreateDriver([FromBody] Driver drive)
    {
        if (drive == null)
            return BadRequest();
        if (drive.Name == string.Empty)
            ModelState.AddModelError("Name", "El driver no debe estar vacio");

        await _driverServices.InsertDriver(drive);

        return Created("Created", true);
    }

    ///Actualizar Driver
    [HttpPut("{Id}")]
    public async Task<IActionResult> UpdateDriver([FromBody] Driver driver, string Id)
    {
        if (driver == null)
            return BadRequest();
        if (driver.Name == string.Empty)
            ModelState.AddModelError("Name", "El driver no debe estar vacio");
        driver.Id = Id;

        await _driverServices.UpdateDriver(driver);
        return Created("Created", true);
    }

    ///Eliminar Driver
    [HttpDelete]
    public async Task<IActionResult> DeleteDriver(string Id)
    {
        await _driverServices.DeleteDriver(Id);
        return NoContent();
    }
}