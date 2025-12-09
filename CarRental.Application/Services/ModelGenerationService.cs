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
    /// <summary>
    /// Create new entity instance and return it's ID
    /// </summary>
    public async Task<int> Create(ModelGenerationCreate entity_dto)
    {
        CarModel model = await modelRepository.Read(entity_dto.ModelId)
            ?? throw new KeyNotFoundException($"CarModel(id={entity_dto.ModelId}) does not exist");

        ModelGeneration entity = mapper.Map<ModelGeneration>(entity_dto);
        entity.Model = model;

        await repository.Create(entity);
        return entity.Id;
    }

    /// <summary>
    /// Update entity's data
    /// </summary>
    public async Task Update(ModelGenerationCreate entity_dto, int generation_id)
    {
        CarModel model = await modelRepository.Read(entity_dto.ModelId)
            ?? throw new KeyNotFoundException($"CarModel(id={entity_dto.ModelId}) does not exist");

        var existing = await repository.Read(generation_id)
            ?? throw new KeyNotFoundException($"ModelGeneration(id={generation_id}) does not exist");

        mapper.Map(entity_dto, existing);
        existing.Model = model;

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
    public async Task<List<ModelGenerationGet>> ReadAll()
    {
        List<ModelGeneration> res = await repository.ReadAll();
        return mapper.Map<List<ModelGenerationGet>>(res);
    }

    /// <summary>
    /// Return entity from storage by id
    /// </summary>
    public async Task<ModelGenerationGet?> Read(int id)
    {
        ModelGeneration? entity = await repository.Read(id);
        return mapper.Map<ModelGenerationGet>(entity);
    }

    /// <summary>
    /// Return linked CarModel's DTO
    /// </summary>
    public async Task<CarModelGet?> GetModel(int generationId)
    {
        ModelGeneration? generation = await repository.Read(generationId);
        if (generation != null)
            return mapper.Map<CarModelGet>(generation.Model);
        return null;
    }
}
