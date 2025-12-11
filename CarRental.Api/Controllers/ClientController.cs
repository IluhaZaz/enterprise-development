using CarRental.Application.Contracts;
using CarRental.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Controllers;

/// <summary>
/// Controller that manage Client entities
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class ClientController(ClientService service, ILogger<ClientController> logger)
    : BaseController<ClientCreate, ClientGet>(service, logger)
{
    /// <summary>
    /// Return all rental logs linked to specified client
    /// </summary>
    [HttpGet("{id}/rentals")]
    public async Task<ActionResult<IList<RentalLogGet>>> GetRentals(int id)
        => await Log(async () =>
        {
            var result = await service.GetRentalLogs(id);
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        });
}