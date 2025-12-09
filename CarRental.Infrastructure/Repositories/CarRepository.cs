using CarRental.Domain.Entities;
using CarRental.Domain.Interfaces;

namespace CarRental.Infrastructure.InMemory.Repositories;

/// <summary>
/// In memory repository for Car entities
/// </summary>
public class CarRepository : IRepository<Car, int>
{
    /// <summary>
    /// Storage for entities
    /// </summary>
    private readonly List<Car> _cars;
    /// <summary>
    /// Id num for next created entity
    /// </summary>
    private int _currId;

    /// <summary>
    /// Repository initializing method
    /// </summary>
    public CarRepository(List<Car>? cars = null)
    {
        if (cars is not null)
        {
            _cars = cars;
            _currId = _cars.Max(car => car.Id) + 1;
        }
        else
        {
            _cars = new List<Car>();
            _currId = 1;
        }
    }

    /// <summary>
    /// Create new entity instance and return it's ID
    /// </summary>
    public Task<int> Create(Car entity)
    {
        entity.Id = _currId;
        _cars.Add(entity);
        _currId++;

        return Task.FromResult(entity.Id);
    }

    /// <summary>
    /// Update entity's data
    /// </summary>
    public Task Update(Car entity)
    {
        Delete(entity.Id);
        _cars.Add(entity);

        return Task.CompletedTask;
    }

    /// <summary>
    /// Delete entity by ID
    /// </summary>
    public Task<bool> Delete(int id)
    {
        Car? car = Read(id).Result;
        if (car != null)
        {
            _cars.Remove(car);
            return Task.FromResult(true);
        }
        return Task.FromResult(false);
    }

    /// <summary>
    /// Return all entities from storage
    /// </summary>
    public Task<List<Car>> ReadAll()
    {
        return Task.FromResult<List<Car>>([.. _cars]);
    }

    /// <summary>
    /// Return entity from storage by id
    /// </summary>
    public Task<Car?> Read(int id)
    {
        return Task.FromResult(_cars.FirstOrDefault(c => c.Id == id));
    }
}
