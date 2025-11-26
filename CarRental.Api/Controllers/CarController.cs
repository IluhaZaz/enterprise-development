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
    [HttpGet("{id}/generation")]
    public ActionResult<CarGet?> GetGeneration(int id)
        => Log(() =>
        {
            ModelGenerationGet? result = service.GetModelGeneration(id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        });
}
