using CarRental.Domain.Entities;
using CarRental.Domain.Interfaces;

namespace CarRental.Infrastructure.Repositories.InMemory;

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
    public CarRepository()
    {
        _cars = new List<Car>();
        _currId = 1;
    }

    /// <summary>
    /// Create new entity instance and return it's ID
    /// </summary>
    public int Create(Car entity)
    {
        entity.Id = _currId;
        _cars.Add(entity);
        _currId++;

        return entity.Id;
    }

    /// <summary>
    /// Update entity's data
    /// </summary>
    public void Update(Car entity)
    {
        Delete(entity.Id);
        _cars.Add(entity);
    }

    /// <summary>
    /// Delete entity by ID
    /// </summary>
    public bool Delete(int id)
    {
        Car? car = Read(id);
        if (car != null)
        {
            _cars.Remove(car);
            return true;
        }
        return false;
    }

    /// <summary>
    /// Return all entities from storage
    /// </summary>
    public List<Car> ReadAll()
    {
        return [.. _cars];
    }

    /// <summary>
    /// Return entity from storage by id
    /// </summary>
    public Car? Read(int id)
    {
        return _cars.FirstOrDefault(c => c.Id == id);
    }
}
