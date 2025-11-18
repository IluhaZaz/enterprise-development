using CarRental.Application.Contracts;
using CarRental.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Controllers;

/// <summary>
/// Controller that manage Car entities
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class CarController(IService<CarCreate, CarGet> service, ILogger<CarController> logger)
    : BaseController<CarCreate, CarGet>(service, logger);
