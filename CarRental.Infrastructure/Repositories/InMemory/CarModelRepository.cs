using CarRental.Domain.Entities;
using CarRental.Domain.Interfaces;

namespace CarRental.Infrastructure.Repositories.InMemory;

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
    public CarModelRepository()
    {
        _carModels = new List<CarModel>();
        _currId = 1;
    }

    /// <summary>
    /// Create new entity instance and return it's ID
    /// </summary>
    public int Create(CarModel entity)
    {
        entity.Id = _currId;
        _carModels.Add(entity);
        _currId++;

        return entity.Id;
    }

    /// <summary>
    /// Update entity's data
    /// </summary>
    public void Update(CarModel entity)
    {
        Delete(entity.Id);
        _carModels.Add(entity);
    }

    /// <summary>
    /// Delete entity by ID
    /// </summary>
    public bool Delete(int id)
    {
        CarModel? carModel = Read(id);
        if (carModel != null)
        {
            _carModels.Remove(carModel);
            return true;
        }
        return false;
    }

    /// <summary>
    /// Return all entities from storage
    /// </summary>
    public List<CarModel> ReadAll()
    {
        return [.. _carModels];
    }

    /// <summary>
    /// Return entity from storage by id
    /// </summary>
    public CarModel? Read(int id)
    {
        return _carModels.FirstOrDefault(c => c.Id == id);
    }
}
