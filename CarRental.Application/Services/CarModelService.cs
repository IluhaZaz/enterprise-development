using AutoMapper;
using CarRental.Application.Contracts;
using CarRental.Application.Interfaces;
using CarRental.Domain.Entities;
using CarRental.Domain.Interfaces;

namespace CarRental.Application.Services;
/// <summary>
/// Provide CRUD operation for CarModel entity
/// </summary>
public class CarModelService(IRepository<CarModel, int> repository, IMapper mapper) : IService<CarModelCreate, CarModelGet>
{
    // <summary>
    /// Create new entity instance and return it's ID
    /// </summary>
    public int Create(CarModelCreate entity_dto)
    {
        CarModel entity = mapper.Map<CarModel>(entity_dto);
        repository.Create(entity);
        return entity.Id;
    }

    /// <summary>
    /// Update entity's data
    /// </summary>
    public void Update(CarModelCreate entity_dto)
    {
        CarModel entity = mapper.Map<CarModel>(entity_dto);
        repository.Update(entity);
    }

    /// <summary>
    /// Delete entity by ID
    /// </summary>
    public bool Delete(int id)
    {
        return repository.Delete(id);
    }

    /// <summary>
    /// Return all entities from storage
    /// </summary>
    public List<CarModelGet> ReadAll()
    {
        List<CarModel> res = repository.ReadAll();
        return mapper.Map<List<CarModelGet>>(res);
    }

    /// <summary>
    /// Return entity from storage by id
    /// </summary>
    public CarModelGet? Read(int id)
    {
        CarModel? entity = repository.Read(id);
        return mapper.Map<CarModelGet>(entity);
    }
}
