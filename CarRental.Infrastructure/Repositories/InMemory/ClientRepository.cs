using CarRental.Domain.Entities;
using CarRental.Domain.Interfaces;

namespace CarRental.Infrastructure.Repositories.InMemory;

public class ClientRepository : IRepository<Client, int>
{
    private readonly List<Client> _clients;
    private int _currId;

    public ClientRepository()
    {
        _clients = new List<Client>();
        _currId = 1;
    }

    public int Create(Client entity)
    {
        entity.Id = _currId;
        _clients.Add(entity);
        _currId++;

        return entity.Id;
    }

    public void Update(Client entity)
    {
        Delete(entity.Id);
        _clients.Add(entity);
    }

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

    public List<Client> ReadAll()
    {
        return [.. _clients];
    }

    public Client? Read(int id)
    {
        return _clients.FirstOrDefault(c => c.Id == id);
    }
}