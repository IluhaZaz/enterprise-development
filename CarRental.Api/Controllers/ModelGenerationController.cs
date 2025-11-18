using CarRental.Application.Contracts;
using CarRental.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Controllers;

/// <summary>
/// Controller that manage ModelGeneration entities
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class ModelGenerationController(IService<ModelGenerationCreate, ModelGenerationGet> service, ILogger<ModelGenerationController> logger)
    : BaseController<ModelGenerationCreate, ModelGenerationGet>(service, logger);
