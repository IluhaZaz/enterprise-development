using CarRental.Application.Contracts;
using CarRental.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Controllers;

/// <summary>
/// Controller that manage RentalLog entities
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class RentalLogController(IService<RentalLogCreate, RentalLogGet> service, ILogger<RentalLogController> logger)
    : BaseController<RentalLogCreate, RentalLogGet>(service, logger);
