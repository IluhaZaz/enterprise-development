using CarRental.Application.Contracts;
using CarRental.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Controllers;

/// <summary>
/// Controller that manage CarModel entities
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class CarModelController(IService<CarModelCreate, CarModelGet> service, ILogger<CarModelController> logger)
    : BaseController<CarModelCreate, CarModelGet>(service, logger);
