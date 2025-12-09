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
    public ActionResult<int> Create([FromBody] TEntityCreateDTO entity_dto)
        => Log(() =>
        {
            int result = -1;
            try
            {
                result = service.Create(entity_dto);
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
    public ActionResult Update(int id, [FromBody] TEntityCreateDTO entity_dto)
        => Log(() =>
        {
            try
            {
                service.Update(entity_dto, id);
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
    public ActionResult<bool> Delete(int id)
        => Log(() =>
        {
            bool result = service.Delete(id);
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
    public ActionResult<List<TEntityGetDTO>> GetAll()
        => Log(() => Ok(service.ReadAll()));


    /// <summary>
    /// Api method for getting entity instance by id
    /// </summary>
    [HttpGet("{id}")]
    public ActionResult<TEntityGetDTO?> Get(int id)
        => Log(() =>
    {
        TEntityGetDTO? result = service.Read(id);
        if (result != null)
        {
            return Ok(result);
        }
        return NotFound();
    });

    /// <summary>
    /// Print data about all incoming requests
    /// </summary>
    protected ActionResult Log(Func<ActionResult> action)
    {
        string method = HttpContext.Request.Method;
        string route = HttpContext.Request.Path;
        string time = DateTime.Now.ToString("HH:mm:ss");

        ActionResult result;
        int code;

        try
        {
            result = action();
            code = 200;
        }
        catch (KeyNotFoundException ex)
        {
            logger.LogError(ex, $"KeyNotFoundException for {method} {route}");
            result = StatusCode(404, $"{ex.Message}\n{ex.InnerException?.Message}");
            code = 404;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"Exception for {method} {route}");
            result = StatusCode(500, $"{ex.Message}\n{ex.InnerException?.Message}");
            code = 500;
        }

        string message = $"{time} {method} {route}: {(result as IStatusCodeActionResult).StatusCode}";
        logger.LogInformation(message);

        return result;
    }
}
