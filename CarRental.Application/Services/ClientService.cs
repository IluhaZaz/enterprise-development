using AutoMapper;
using CarRental.Application.Contracts;
using CarRental.Application.Interfaces;
using CarRental.Domain.Entities;
using CarRental.Domain.Interfaces;

namespace CarRental.Application.Services;
/// <summary>
/// Provide CRUD operation for Client entity
/// </summary>
public class ClientService(
    IRepository<Client, int> repository, 
    IRepository<RentalLog, int> 
    rentalRepository, 
    IMapper mapper) : IService<ClientCreate, ClientGet>
{
    /// <summary>
    /// Create new entity instance and return it's ID
    /// </summary>
    public async Task<int> Create(ClientCreate entity_dto)
    {
        Client entity = mapper.Map<Client>(entity_dto);
        await repository.Create(entity);
        return entity.Id;
    }

    /// <summary>
    /// Update entity's data
    /// </summary>
    public async Task Update(ClientCreate entity_dto, int client_id)
    {
        var existing = await repository.Read(client_id)
            ?? throw new KeyNotFoundException($"Client(id={client_id}) does not exist");

        mapper.Map(entity_dto, existing);

        await repository.Update(existing);
    }

    /// <summary>
    /// Delete entity by ID
    /// </summary>
    public async Task<bool> Delete(int id)
    {
        return await repository.Delete(id);
    }

    /// <summary>
    /// Return all entities from storage
    /// </summary>
    public async Task<List<ClientGet>> ReadAll()
    {
        List<Client> res = await repository.ReadAll();
        return mapper.Map<List<ClientGet>>(res);
    }

    /// <summary>
    /// Return entity from storage by id
    /// </summary>
    public async Task<ClientGet?> Read(int id)
    {
        Client? entity = await repository.Read(id);
        return mapper.Map<ClientGet>(entity);
    }

    /// <summary>
    /// Return all rental logs linked to specified client
    /// </summary>
    public async Task<IList<RentalLogGet>?> GetRentalLogs(int clientId)
    {
        Client? client = await repository.Read(clientId);
        if (client != null)
        {
            var rentals = await rentalRepository.ReadAll();

            var clientRentals = rentals
                .Where(r => r.ClientId == clientId)
                .ToList();

            return mapper.Map<IList<RentalLogGet>>(clientRentals);
        }

        return null;
    }
}
