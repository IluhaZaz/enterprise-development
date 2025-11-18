using CarRental.Domain.Entities;
using CarRental.Domain.Interfaces;

namespace CarRental.Infrastructure.Repositories.InMemory;

/// <summary>
/// In memory repository for RentalLog entities
/// </summary>
public class RentalLogRepository : IRepository<RentalLog, int>
{
    /// <summary>
    /// Storage for entities
    /// </summary>
    private readonly List<RentalLog> _rentalLogs;
    /// <summary>
    /// Id num for next created entity
    /// </summary>
    private int _currId;

    /// <summary>
    /// Repository initializing method
    /// </summary>
    public RentalLogRepository()
    {
        _rentalLogs = new List<RentalLog>();
        _currId = 1;
    }

    /// <summary>
    /// Create new entity instance and return it's ID
    /// </summary>
    public int Create(RentalLog entity)
    {
        entity.Id = _currId;
        _rentalLogs.Add(entity);
        _currId++;

        return entity.Id;
    }

    /// <summary>
    /// Update entity's data
    /// </summary>
    public void Update(RentalLog entity)
    {
        Delete(entity.Id);
        _rentalLogs.Add(entity);
    }

    /// <summary>
    /// Delete entity by ID
    /// </summary>
    public bool Delete(int id)
    {
        RentalLog? rentalLog = Read(id);
        if (rentalLog != null)
        {
            _rentalLogs.Remove(rentalLog);
            return true;
        }
        return false;
    }

    /// <summary>
    /// Return all entities from storage
    /// </summary>
    public List<RentalLog> ReadAll()
    {
        return [.. _rentalLogs];
    }

    /// <summary>
    /// Return entity from storage by id
    /// </summary>
    public RentalLog? Read(int id)
    {
        return _rentalLogs.FirstOrDefault(c => c.Id == id);
    }
}
