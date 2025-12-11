using CarRental.Application.Contracts;
using CarRental.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Controllers;

/// <summary>
/// Provides API endpoints for analytical operations
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class AnalyticsController(IAnalyticsService analytics, ILogger<AnalyticsController> logger)
    : ControllerBase
{
    /// <summary>
    /// Returns all clients who rented cars of the specified model with rent counts ordered by full name
    /// </summary>
    [HttpGet("clients-by-model/{modelId}")]
    public async Task<ActionResult<IList<ClientModelRentStat>>> GetClientsByCarModel(int modelId)
    {
        var result = await analytics.GetClientsByCarModel(modelId);
        return Ok(result);
    }

    /// <summary>
    /// Returns all cars that are currently in rent
    /// </summary>
    [HttpGet("cars-in-rent")]
    public async Task<ActionResult<IList<CarGet>>> GetCarsInRent([FromQuery] DateTime? time = null)
    {
        var current = time ?? DateTime.UtcNow;
        var result = await analytics.GetCarsInRent(current);
        return Ok(result);
    }

    /// <summary>
    /// Returns the top 5 most frequently rented cars
    /// </summary>
    [HttpGet("top-cars")]
    public async Task<ActionResult<IList<CarGet>>> GetTopFiveCars()
    {
        var result = await analytics.GetTopFiveCars();
        return Ok(result);
    }

    /// <summary>
    /// Returns rental count summary for each car
    /// </summary>
    [HttpGet("rent-count")]
    public async Task<ActionResult<IList<CarRentCountStat>>> GetRentCountPerCar()
    {
        var result = await analytics.GetRentCountPerCar();
        return Ok(result);
    }

    /// <summary>
    /// Returns the top 5 clients by total rental cost
    /// </summary>
    [HttpGet("top-clients")]
    public async Task<ActionResult<IList<ClientRentAmountStat>>> GetTopFiveClients()
    {
        var result = await analytics.GetTopFiveClientsByRent();
        return Ok(result);
    }
}