using AutoMapper;
using CarRental.Application.Contracts;
using CarRental.Application.Interfaces;
using CarRental.Domain.Entities;
using CarRental.Domain.Interfaces;

namespace CarRental.Application.Services;
public class ClientService(IRepository<Client, int> repository, IMapper mapper) : IService<ClientCreate, ClientGet>
{
    public int Create(ClientCreate entity_dto)
    {
        Client entity = mapper.Map<Client>(entity_dto);
        repository.Create(entity);
        return entity.Id;
    }

    public void Update(ClientCreate entity_dto)
    {
        Client entity = mapper.Map<Client>(entity_dto);
        repository.Update(entity);
    }

    public bool Delete(int id)
    {
        return repository.Delete(id);
    }

    public List<ClientGet> ReadAll()
    {
        List<Client> res = repository.ReadAll();
        return mapper.Map<List<ClientGet>>(res);
    }

    public ClientGet? Read(int id)
    {
        Client? entity = repository.Read(id);
        return mapper.Map<ClientGet>(entity);
    }
}
