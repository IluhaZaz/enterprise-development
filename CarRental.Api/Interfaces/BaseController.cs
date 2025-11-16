using Microsoft.AspNetCore.Mvc;
using CarRental.Application.Interfaces;

namespace CarRental.Api.Interfaces;

[Route("api/[controller]")]
[ApiController]
public class BaseController<TEntityCreateDTO, TEntityGetDTO>(
    IService<TEntityCreateDTO, TEntityGetDTO> service,
    ILogger<BaseController<TEntityCreateDTO, TEntityGetDTO>> logger)
    : ControllerBase
{
    [HttpPost]
    public ActionResult<int> Create([FromBody] TEntityCreateDTO entity_dto)
    {
        int result = service.Create(entity_dto);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public ActionResult Update([FromBody] TEntityCreateDTO entity_dto)
    {
        try
        {
            service.Update(entity_dto);
            return Ok();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpDelete("{id}")]
    public ActionResult<bool> Delete(int id)
    {
        bool result = service.Delete(id);
        if (result)
        {
            return Ok();
        }
        return NotFound();
    }

    [HttpGet]
    public ActionResult<List<TEntityGetDTO>> GetAll()
    {
        List<TEntityGetDTO> result = service.ReadAll();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public ActionResult<TEntityGetDTO?> Get(int id)
    {
        TEntityGetDTO? result = service.Read(id);
        if (result != null)
            return Ok(result);
        return NotFound();
    }
}
