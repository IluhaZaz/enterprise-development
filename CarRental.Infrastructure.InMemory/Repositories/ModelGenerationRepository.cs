using CarRental.Domain.Entities;
using CarRental.Domain.Interfaces;

namespace CarRental.Infrastructure.InMemory.Repositories;

/// <summary>
/// In memory repository for ModelGeneration entities
/// </summary>
public class ModelGenerationRepository : IRepository<ModelGeneration, int>
{
    /// <summary>
    /// Storage for entities
    /// </summary>
    private readonly List<ModelGeneration> _modelGenerations;

    /// <summary>
    /// Id num for next created entity
    /// </summary>
    private int _currId;

    /// <summary>
    /// Repository initializing method
    /// </summary>
    public ModelGenerationRepository(List<ModelGeneration>? generations = null)
    {
        if (generations is not null)
        {
            _modelGenerations = generations;
            _currId = _modelGenerations.Max(car => car.Id) + 1;
        }
        else
        {
            _modelGenerations = new List<ModelGeneration>();
            _currId = 1;
        }
    }

    /// <summary>
    /// Create new entity instance and return it's ID
    /// </summary>
    public Task<int> Create(ModelGeneration entity)
    {
        entity.Id = _currId;
        _modelGenerations.Add(entity);
        _currId++;

        return Task.FromResult(entity.Id);
    }

    /// <summary>
    /// Update entity's data
    /// </summary>
    public Task Update(ModelGeneration entity)
    {
        Delete(entity.Id);
        _modelGenerations.Add(entity);

        return Task.CompletedTask;
    }

    /// <summary>
    /// Delete entity by ID
    /// </summary>
    public Task<bool> Delete(int id)
    {
        ModelGeneration? modelGeneration = Read(id).Result;
        if (modelGeneration != null)
        {
            _modelGenerations.Remove(modelGeneration);
            return Task.FromResult(true);
        }
        return Task.FromResult(false);
    }

    /// <summary>
    /// Return all entities from storage
    /// </summary>
    public Task<List<ModelGeneration>> ReadAll()
    {
        return Task.FromResult(_modelGenerations.ToList());
    }

    /// <summary>
    /// Return entity from storage by id
    /// </summary>
    public Task<ModelGeneration?> Read(int id)
    {
        return Task.FromResult(_modelGenerations.FirstOrDefault(c => c.Id == id));
    }
}
