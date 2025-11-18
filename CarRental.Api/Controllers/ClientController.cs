using CarRental.Application.Contracts;
using CarRental.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Controllers;

/// <summary>
/// Controller that manage Client entities
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class ClientController(IService<ClientCreate, ClientGet> service, ILogger<ClientController> logger)
    : BaseController<ClientCreate, ClientGet>(service, logger);
