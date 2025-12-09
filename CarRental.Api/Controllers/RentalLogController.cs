using CarRental.Application.Contracts;
using CarRental.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Controllers;

/// <summary>
/// Controller that manage RentalLog entities
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class RentalLogController(RentalLogService service, ILogger<RentalLogController> logger)
    : BaseController<RentalLogCreate, RentalLogGet>(service, logger)
{
    /// <summary>
    /// Return linked Client's DTO
    /// </summary>
    [HttpGet("{id}/client")]
    public async Task<ActionResult<ClientGet?>> GetClient(int id)
        => await Log(async () =>
        {
            ClientGet? result = await service.GetClient(id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        });

    /// <summary>
    /// Return linked Car's DTO
    /// </summary>
    [HttpGet("{id}/car")]
    public async Task<ActionResult<CarGet?>> GetCar(int id)
        => await Log(async () =>
        {
            CarGet? result = await service.GetCar(id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        });
}
