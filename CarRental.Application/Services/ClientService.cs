using AutoMapper;
using CarRental.Application.Contracts;
using CarRental.Application.Interfaces;
using CarRental.Domain.Entities;
using CarRental.Domain.Interfaces;

namespace CarRental.Application.Services;
/// <summary>
/// Provide CRUD operation for Client entity
/// </summary>
public class ClientService(IRepository<Client, int> repository, IMapper mapper) : IService<ClientCreate, ClientGet>
{
    // <summary>
    /// Create new entity instance and return it's ID
    /// </summary>
    public int Create(ClientCreate entity_dto)
    {
        Client entity = mapper.Map<Client>(entity_dto);
        repository.Create(entity);
        return entity.Id;
    }

    /// <summary>
    /// Update entity's data
    /// </summary>
    public void Update(ClientCreate entity_dto, int client_id)
    {
        var existing = repository.Read(client_id)
            ?? throw new KeyNotFoundException($"Client(id={client_id}) does not exist");

        mapper.Map(entity_dto, existing);

        repository.Update(existing);
    }

    /// <summary>
    /// Delete entity by ID
    /// </summary>
    public bool Delete(int id)
    {
        return repository.Delete(id);
    }

    /// <summary>
    /// Return all entities from storage
    /// </summary>
    public List<ClientGet> ReadAll()
    {
        List<Client> res = repository.ReadAll();
        return mapper.Map<List<ClientGet>>(res);
    }

    /// <summary>
    /// Return entity from storage by id
    /// </summary>
    public ClientGet? Read(int id)
    {
        Client? entity = repository.Read(id);
        return mapper.Map<ClientGet>(entity);
    }
}
