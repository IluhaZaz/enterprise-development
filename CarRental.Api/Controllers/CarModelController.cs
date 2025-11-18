using Microsoft.AspNetCore.Mvc;
using CarRental.Application.Interfaces;
using CarRental.Application.Contracts;

namespace CarRental.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CarModelController(IService<CarModelCreate, CarModelGet> service, ILogger<CarModelController> logger)
    : BaseController<CarModelCreate, CarModelGet>(service, logger);
