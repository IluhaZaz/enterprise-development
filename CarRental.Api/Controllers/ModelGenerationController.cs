using Microsoft.AspNetCore.Mvc;
using CarRental.Application.Interfaces;
using CarRental.Application.Contracts;
using CarRental.Api.Interfaces;

namespace CarRental.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ModelGenerationController(IService<ModelGenerationCreate, ModelGenerationGet> service, ILogger<ModelGenerationController> logger)
    : BaseController<ModelGenerationCreate, ModelGenerationGet>(service, logger);
