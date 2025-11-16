using Microsoft.AspNetCore.Mvc;
using CarRental.Application.Interfaces;
using CarRental.Application.Contracts;
using CarRental.Api.Interfaces;

namespace CarRental.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RentalLogController(IService<RentalLogCreate, RentalLogGet> service, ILogger<RentalLogController> logger)
    : BaseController<RentalLogCreate, RentalLogGet>(service, logger);
