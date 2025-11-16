using AutoMapper;
using CarRental.Application.Contracts;
using CarRental.Application.Interfaces;
using CarRental.Domain.Entities;
using CarRental.Domain.Interfaces;

namespace CarRental.Application.Services;
public class CarModelService(IRepository<CarModel, int> repository, IMapper mapper) : IService<CarModelCreate, CarModelGet>
{
    public int Create(CarModelCreate entity_dto)
    {
        CarModel entity = mapper.Map<CarModel>(entity_dto);
        repository.Create(entity);
        return entity.Id;
    }

    public void Update(CarModelCreate entity_dto)
    {
        CarModel entity = mapper.Map<CarModel>(entity_dto);
        repository.Update(entity);
    }

    public bool Delete(int id)
    {
        return repository.Delete(id);
    }

    public List<CarModelGet> ReadAll()
    {
        List<CarModel> res = repository.ReadAll();
        return mapper.Map<List<CarModelGet>>(res);
    }

    public CarModelGet? Read(int id)
    {
        CarModel? entity = repository.Read(id);
        return mapper.Map<CarModelGet>(entity);
    }
}
