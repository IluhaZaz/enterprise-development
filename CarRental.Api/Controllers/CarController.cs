using Microsoft.AspNetCore.Mvc;
using CarRental.Application.Interfaces;
using CarRental.Application.Contracts;
using CarRental.Api.Interfaces;

namespace CarRental.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CarController(IService<CarCreate, CarGet> service, ILogger<CarController> logger)
    : BaseController<CarCreate, CarGet>(service, logger);
