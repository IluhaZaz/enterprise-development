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
    public ActionResult<ClientGet?> GetClient(int id)
        => Log(() =>
        {
            ClientGet? result = service.GetClient(id);
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
    public ActionResult<CarGet?> GetCar(int id)
        => Log(() =>
        {
            CarGet? result = service.GetCar(id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        });
}
