using AutoMapper;
using CarRental.Application.Contracts;
using CarRental.Application.Interfaces;
using CarRental.Domain.Entities;
using CarRental.Domain.Interfaces;

namespace CarRental.Application.Services;
/// <summary>
/// Provide CRUD operation for ModelGeneration entity
/// </summary>
public class ModelGenerationService(
    IRepository<ModelGeneration, int> repository,
    IRepository<CarModel, int> modelRepository,
    IMapper mapper) : IService<ModelGenerationCreate, ModelGenerationGet>
{
    // <summary>
    /// Create new entity instance and return it's ID
    /// </summary>
    public int Create(ModelGenerationCreate entity_dto)
    {
        CarModel model = modelRepository.Read(entity_dto.ModelId)
             ?? throw new KeyNotFoundException($"CarModel(id={entity_dto.ModelId}) does not exist");

        ModelGeneration entity = mapper.Map<ModelGeneration>(entity_dto);
        entity.Model = model;

        repository.Create(entity);
        return entity.Id;
    }

    /// <summary>
    /// Update entity's data
    /// </summary>
    public void Update(ModelGenerationCreate entity_dto)
    {
        CarModel model = modelRepository.Read(entity_dto.ModelId)
             ?? throw new KeyNotFoundException($"CarModel(id={entity_dto.ModelId}) does not exist");

        ModelGeneration entity = mapper.Map<ModelGeneration>(entity_dto);
        entity.Model = model;

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
    public List<ModelGenerationGet> ReadAll()
    {
        List<ModelGeneration> res = repository.ReadAll();
        return mapper.Map<List<ModelGenerationGet>>(res);
    }

    /// <summary>
    /// Return entity from storage by id
    /// </summary>
    public ModelGenerationGet? Read(int id)
    {
        ModelGeneration? entity = repository.Read(id);
        return mapper.Map<ModelGenerationGet>(entity);
    }

    public CarModelGet? GetModel(int generationId)
    {
        ModelGeneration? generation = repository.Read(generationId);
        if (generation != null)
            return mapper.Map<CarModelGet>(generation.Model);
        return null;
    }
}
