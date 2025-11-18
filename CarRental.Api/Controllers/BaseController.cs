using CarRental.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Controllers;

/// <summary>
/// Base class for controller that manage entities
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class BaseController<TEntityCreateDTO, TEntityGetDTO>(
    IService<TEntityCreateDTO, TEntityGetDTO> service,
    ILogger<BaseController<TEntityCreateDTO, TEntityGetDTO>> logger)
    : ControllerBase
{
    /// <summary>
    /// Api method for creating new entity instance
    /// </summary>
    [HttpPost]
    public ActionResult<int> Create([FromBody] TEntityCreateDTO entity_dto)
    {
        int result = -1;
        try
        {
            result = service.Create(entity_dto);
        }
        catch (KeyNotFoundException ex)
        {
            Log(404);
            return NotFound(ex.Message);
        }
        Log(200);
        return Ok(result);
    }

    /// <summary>
    /// Api method for updating entity data
    /// </summary>
    [HttpPut("{id}")]
    public ActionResult Update([FromBody] TEntityCreateDTO entity_dto)
    {
        try
        {
            service.Update(entity_dto);
            Log(200);
            return Ok();
        }
        catch (KeyNotFoundException)
        {
            Log(404);
            return NotFound();
        }
    }

    /// <summary>
    /// Api method for deleting entity by id
    /// </summary>
    [HttpDelete("{id}")]
    public ActionResult<bool> Delete(int id)
    {
        bool result = service.Delete(id);
        if (result)
        {
            Log(200);
            return Ok();
        }
        Log(404);
        return NotFound();
    }

    /// <summary>
    /// Api method for getting all entity instances
    /// </summary>
    [HttpGet]
    public ActionResult<List<TEntityGetDTO>> GetAll()
    {
        List<TEntityGetDTO> result = service.ReadAll();
        Log(200);
        return Ok(result);
    }

    /// <summary>
    /// Api method for getting entity instance by id
    /// </summary>
    [HttpGet("{id}")]
    public ActionResult<TEntityGetDTO?> Get(int id)
    {
        TEntityGetDTO? result = service.Read(id);
        if (result != null)
        {
            Log(200);
            return Ok(result);
        }
        Log(404);
        return NotFound();
    }

    /// <summary>
    /// Print data about all incoming requests
    /// </summary>
    protected void Log(int code)
    {
        string method = HttpContext.Request.Method;
        string route = HttpContext.Request.Path;
        DateTime timestamp = DateTime.Now;

        string message = $"{timestamp} {method} {route}: {code}";

        if (200 <= code && code < 300)
        {
            logger.LogInformation(message);
        }
        else if (400 <= code)
        {
            logger.LogError(message);
        }
        else
        {
            logger.LogDebug(message);
        }
    }
}
