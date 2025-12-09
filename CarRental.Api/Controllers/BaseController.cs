using CarRental.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

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
    public async Task<ActionResult<int>> Create([FromBody] TEntityCreateDTO entity_dto)
        => await Log(async () =>
        {
            var result = -1;
            try
            {
                result = await service.Create(entity_dto);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            return Ok(result);
        });

    /// <summary>
    /// Api method for updating entity data
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] TEntityCreateDTO entity_dto)
        => await Log(async () =>
        {
            try
            {
                await service.Update(entity_dto, id);
                return Ok();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        });

    /// <summary>
    /// Api method for deleting entity by id
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> Delete(int id)
        => await Log(async () =>
        {
            var result = await service.Delete(id);
            if (result)
            {
                return Ok();
            }
            return NotFound();
        });

    /// <summary>
    /// Api method for getting all entity instances
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<TEntityGetDTO>>> GetAll()
        => await Log(async () => Ok(await service.ReadAll()));

    /// <summary>
    /// Api method for getting entity instance by id
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<TEntityGetDTO?>> Get(int id)
        => await Log(async () =>
        {
            TEntityGetDTO? result = await service.Read(id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        });

    /// <summary>
    /// Print data about all incoming requests
    /// </summary>
    protected async Task<ActionResult> Log(Func<Task<ActionResult>> action)
    {
        var method = HttpContext.Request.Method;
        var route = HttpContext.Request.Path;
        var time = DateTime.Now.ToString("HH:mm:ss");

        ActionResult result;

        try
        {
            result = await action();
        }
        catch (KeyNotFoundException ex)
        {
            logger.LogError(ex, $"KeyNotFoundException for {method} {route}");
            result = StatusCode(404, $"{ex.Message}\n{ex.InnerException?.Message}");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"Exception for {method} {route}");
            result = StatusCode(500, $"{ex.Message}\n{ex.InnerException?.Message}");
        }

        var message = $"{time} {method} {route}: {(result as IStatusCodeActionResult)?.StatusCode}";
        logger.LogInformation(message);

        return result;
    }
}
