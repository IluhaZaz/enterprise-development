using CarRental.Domain.Entities;
using CarRental.Domain.Interfaces;

namespace CarRental.Infrastructure.InMemory.Repositories;

/// <summary>
/// In memory repository for Client entities
/// </summary>
public class ClientRepository : IRepository<Client, int>
{
    /// <summary>
    /// Storage for entities
    /// </summary>
    private readonly List<Client> _clients;
    /// <summary>
    /// Id num for next created entity
    /// </summary>
    private int _currId;

    /// <summary>
    /// Repository initializing method
    /// </summary>
    public ClientRepository(List<Client>? clients = null)
    {
        if (clients is not null)
        {
            _clients = clients;
            _currId = _clients.Max(car => car.Id) + 1;
        }
        else
        {
            _clients = new List<Client>();
            _currId = 1;
        }
    }

    /// <summary>
    /// Create new entity instance and return it's ID
    /// </summary>
    public Task<int> Create(Client entity)
    {
        entity.Id = _currId;
        _clients.Add(entity);
        _currId++;

        return Task.FromResult(entity.Id);
    }

    /// <summary>
    /// Update entity's data
    /// </summary>
    public Task Update(Client entity)
    {
        Delete(entity.Id);
        _clients.Add(entity);

        return Task.CompletedTask;
    }

    /// <summary>
    /// Delete entity by ID
    /// </summary>
    public Task<bool> Delete(int id)
    {
        Client? client = Read(id).Result;
        if (client != null)
        {
            _clients.Remove(client);
            return Task.FromResult(true);
        }
        return Task.FromResult(false);
    }

    /// <summary>
    /// Return all entities from storage
    /// </summary>
    public Task<List<Client>> ReadAll()
    {
        return Task.FromResult<List<Client>>([.. _clients]);
    }

    /// <summary>
    /// Return entity from storage by id
    /// </summary>
    public Task<Client?> Read(int id)
    {
        return Task.FromResult(_clients.FirstOrDefault(c => c.Id == id));
    }
}