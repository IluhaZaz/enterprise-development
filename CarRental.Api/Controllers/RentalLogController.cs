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
