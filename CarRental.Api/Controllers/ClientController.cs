using Microsoft.AspNetCore.Mvc;
using CarRental.Application.Interfaces;
using CarRental.Application.Contracts;
using CarRental.Api.Interfaces;

namespace CarRental.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientController(IService<ClientCreate, ClientGet> service, ILogger<ClientController> logger)
    : BaseController<ClientCreate, ClientGet>(service, logger);
