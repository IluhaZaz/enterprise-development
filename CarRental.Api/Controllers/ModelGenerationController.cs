using CarRental.Application.Contracts;
using CarRental.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Controllers;

/// <summary>
/// Controller that manage ModelGeneration entities
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class ModelGenerationController(ModelGenerationService service, ILogger<ModelGenerationController> logger)
    : BaseController<ModelGenerationCreate, ModelGenerationGet>(service, logger)
{
    /// <summary>
    /// Return linked CarModel's DTO
    /// </summary>
    [HttpGet("{id}/model")]
    public async Task<ActionResult<CarModelGet?>> GetModel(int id)
        => await Log(async () =>
        {
            CarModelGet? result = await service.GetModel(id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        });
}
