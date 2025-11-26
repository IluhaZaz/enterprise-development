using CarRental.Domain.Entities;
using CarRental.Domain.Interfaces;

namespace CarRental.Infrastructure.Repositories;

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
    public int Create(Client entity)
    {
        entity.Id = _currId;
        _clients.Add(entity);
        _currId++;

        return entity.Id;
    }

    /// <summary>
    /// Update entity's data
    /// </summary>
    public void Update(Client entity)
    {
        Delete(entity.Id);
        _clients.Add(entity);
    }

    /// <summary>
    /// Delete entity by ID
    /// </summary>
    public bool Delete(int id)
    {
        Client? client = Read(id);
        if (client != null)
        {
            _clients.Remove(client);
            return true;
        }
        return false;
    }

    /// <summary>
    /// Return all entities from storage
    /// </summary>
    public List<Client> ReadAll()
    {
        return [.. _clients];
    }

    /// <summary>
    /// Return entity from storage by id
    /// </summary>
    public Client? Read(int id)
    {
        return _clients.FirstOrDefault(c => c.Id == id);
    }
}