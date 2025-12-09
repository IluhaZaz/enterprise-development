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
    /// <summary>
    /// Create new entity instance and return it's ID
    /// </summary>
    public async Task<int> Create(CarModelCreate entity_dto)
    {
        CarModel entity = mapper.Map<CarModel>(entity_dto);
        await repository.Create(entity);
        return entity.Id;
    }

    /// <summary>
    /// Update entity's data
    /// </summary>
    public async Task Update(CarModelCreate entity_dto, int model_id)
    {
        var existing = await repository.Read(model_id)
            ?? throw new KeyNotFoundException($"Model(id={model_id}) does not exist");

        mapper.Map(entity_dto, existing);

        await repository.Update(existing);
    }

    /// <summary>
    /// Delete entity by ID
    /// </summary>
    public async Task<bool> Delete(int id)
    {
        return await repository.Delete(id);
    }

    /// <summary>
    /// Return all entities from storage
    /// </summary>
    public async Task<List<CarModelGet>> ReadAll()
    {
        List<CarModel> res = await repository.ReadAll();
        return mapper.Map<List<CarModelGet>>(res);
    }

    /// <summary>
    /// Return entity from storage by id
    /// </summary>
    public async Task<CarModelGet?> Read(int id)
    {
        CarModel? entity = await repository.Read(id);
        return mapper.Map<CarModelGet>(entity);
    }
}
