using CarRental.Domain.Entities;
using CarRental.Domain.Interfaces;

namespace CarRental.Infrastructure.InMemory.Repositories;

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
    public RentalLogRepository(List<RentalLog>? logs = null)
    {
        if (logs is not null)
        {
            _rentalLogs = logs;
            _currId = _rentalLogs.Max(car => car.Id) + 1;
        }
        else
        {
            _rentalLogs = new List<RentalLog>();
            _currId = 1;
        }
    }

    /// <summary>
    /// Create new entity instance and return it's ID
    /// </summary>
    public Task<int> Create(RentalLog entity)
    {
        entity.Id = _currId;
        _rentalLogs.Add(entity);
        _currId++;

        return Task.FromResult(entity.Id);
    }

    /// <summary>
    /// Update entity's data
    /// </summary>
    public Task Update(RentalLog entity)
    {
        Delete(entity.Id);
        _rentalLogs.Add(entity);

        return Task.CompletedTask;
    }

    /// <summary>
    /// Delete entity by ID
    /// </summary>
    public Task<bool> Delete(int id)
    {
        RentalLog? rentalLog = Read(id).Result;
        if (rentalLog != null)
        {
            _rentalLogs.Remove(rentalLog);
            return Task.FromResult(true);
        }
        return Task.FromResult(false);
    }

    /// <summary>
    /// Return all entities from storage
    /// </summary>
    public Task<List<RentalLog>> ReadAll()
    {
        return Task.FromResult(_rentalLogs.ToList());
    }

    /// <summary>
    /// Return entity from storage by id
    /// </summary>
    public Task<RentalLog?> Read(int id)
    {
        return Task.FromResult(_rentalLogs.FirstOrDefault(c => c.Id == id));
    }
}
