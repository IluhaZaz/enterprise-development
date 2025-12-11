using CarRental.Application.Contracts;
using CarRental.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Controllers;

/// <summary>
/// Controller that manage Car entities
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class CarController(CarService service, ILogger<CarController> logger)
    : BaseController<CarCreate, CarGet>(service, logger)
{
    /// <summary>
    /// Return linked ModelGeneration's DTO
    /// </summary>
    [HttpGet("{id}/generation")]
    public async Task<ActionResult<CarGet?>> GetGeneration(int id)
        => await Log(async () =>
        {
            ModelGenerationGet? result = await service.GetModelGeneration(id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        });

    /// <summary>
    /// Return all rental logs linked to specified car
    /// </summary>
    [HttpGet("{id}/rentals")]
    public async Task<ActionResult<IList<RentalLogGet>>> GetRentals(int id)
        => await Log(async () =>
        {
            var result = await service.GetRentalLogs(id);
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        });
}
