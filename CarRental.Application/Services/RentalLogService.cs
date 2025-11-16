using CarRental.Application.Interfaces;
using CarRental.Infrastructure.Repositories.InMemory;
using CarRental.Application.Contracts;
using AutoMapper;
using CarRental.Domain.Entities;

namespace CarRental.Application.Services;
public class RentalLogService(RentalLogRepository repository, IMapper mapper) : IService<RentalLogCreate, RentalLogGet>
{
    public int Create(RentalLogCreate entity_dto)
    {
        RentalLog entity = mapper.Map<RentalLog>(entity_dto);
        repository.Create(entity);
        return entity.Id;
    }

    public void Update(RentalLogCreate entity_dto)
    {
        RentalLog entity = mapper.Map<RentalLog>(entity_dto);
        repository.Update(entity);
    }

    public bool Delete(int id)
    {
        return repository.Delete(id);
    }

    public List<RentalLogGet> ReadAll()
    {
        List<RentalLog> res = repository.ReadAll();
        return mapper.Map<List<RentalLogGet>>(res);
    }

    public RentalLogGet? Read(int id)
    {
        RentalLog? entity = repository.Read(id);
        return mapper.Map<RentalLogGet>(entity);
    }
}
