using CarRental.Domain.Entities;
using CarRental.Domain.Interfaces;

namespace CarRental.Infrastructure.InMemory.Repositories;

/// <summary>
/// In memory repository for CarModel entities
/// </summary>
public class CarModelRepository : IRepository<CarModel, int>
{
    /// <summary>
    /// Storage for entities
    /// </summary>
    private readonly List<CarModel> _carModels;
    /// <summary>
    /// Id num for next created entity
    /// </summary>
    private int _currId;

    /// <summary>
    /// Repository initializing method
    /// </summary>
    public CarModelRepository(List<CarModel>? models = null)
    {
        if (models is not null)
        {
            _carModels = models;
            _currId = _carModels.Max(car => car.Id) + 1;
        }
        else
        {
            _carModels = new List<CarModel>();
            _currId = 1;
        }
    }

    /// <summary>
    /// Create new entity instance and return it's ID
    /// </summary>
    public Task<int> Create(CarModel entity)
    {
        entity.Id = _currId;
        _carModels.Add(entity);
        _currId++;

        return Task.FromResult(entity.Id);
    }

    /// <summary>
    /// Update entity's data
    /// </summary>
    public Task Update(CarModel entity)
    {
        Delete(entity.Id);
        _carModels.Add(entity);

        return Task.CompletedTask;
    }

    /// <summary>
    /// Delete entity by ID
    /// </summary>
    public Task<bool> Delete(int id)
    {
        CarModel? carModel = Read(id).Result;
        if (carModel != null)
        {
            _carModels.Remove(carModel);
            return Task.FromResult(true);
        }
        return Task.FromResult(false);
    }

    /// <summary>
    /// Return all entities from storage
    /// </summary>
    public Task<List<CarModel>> ReadAll()
    {
        return Task.FromResult<List<CarModel>>([.. _carModels]);
    }

    /// <summary>
    /// Return entity from storage by id
    /// </summary>
    public Task<CarModel?> Read(int id)
    {
        return Task.FromResult(_carModels.FirstOrDefault(c => c.Id == id));
    }
}
