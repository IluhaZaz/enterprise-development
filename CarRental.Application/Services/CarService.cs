using CarRental.Application.Interfaces;
using CarRental.Infrastructure.Repositories.InMemory;
using CarRental.Application.Contracts;
using AutoMapper;
using CarRental.Domain.Entities;

namespace CarRental.Application.Services;
public class CarService(CarRepository repository, IMapper mapper) : IService<CarCreate, CarGet>
{
    public int Create(CarCreate entity_dto)
    {
        Car entity = mapper.Map<Car>(entity_dto);
        repository.Create(entity);
        return entity.Id;
    }

    public void Update(CarCreate entity_dto)
    {
        Car entity = mapper.Map<Car>(entity_dto);
        repository.Update(entity);
    }

    public bool Delete(int id)
    {
        return repository.Delete(id);
    }

    public List<CarGet> ReadAll()
    {
        List<Car> res = repository.ReadAll();
        return mapper.Map<List<CarGet>>(res);
    }

    public CarGet? Read(int id)
    {
        Car? entity = repository.Read(id);
        return mapper.Map<CarGet>(entity);
    }
}
