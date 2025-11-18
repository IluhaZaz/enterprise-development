using CarRental.Domain.Entities;
using CarRental.Domain.Interfaces;

namespace CarRental.Infrastructure.Repositories.InMemory;

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
    public ModelGenerationRepository()
    {
        _modelGenerations = new List<ModelGeneration>();
        _currId = 1;
    }

    /// <summary>
    /// Create new entity instance and return it's ID
    /// </summary>
    public int Create(ModelGeneration entity)
    {
        entity.Id = _currId;
        _modelGenerations.Add(entity);
        _currId++;

        return entity.Id;
    }

    /// <summary>
    /// Update entity's data
    /// </summary>
    public void Update(ModelGeneration entity)
    {
        Delete(entity.Id);
        _modelGenerations.Add(entity);
    }

    /// <summary>
    /// Delete entity by ID
    /// </summary>
    public bool Delete(int id)
    {
        ModelGeneration? modelGeneration = Read(id);
        if (modelGeneration != null)
        {
            _modelGenerations.Remove(modelGeneration);
            return true;
        }
        return false;
    }

    /// <summary>
    /// Return all entities from storage
    /// </summary>
    public List<ModelGeneration> ReadAll()
    {
        return [.. _modelGenerations];
    }

    /// <summary>
    /// Return entity from storage by id
    /// </summary>
    public ModelGeneration? Read(int id)
    {
        return _modelGenerations.FirstOrDefault(c => c.Id == id);
    }
}
