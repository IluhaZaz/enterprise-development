using AutoMapper;
using CarRental.Application.Contracts;
using CarRental.Application.Interfaces;
using CarRental.Domain.Entities;
using CarRental.Domain.Interfaces;

namespace CarRental.Application.Services;
public class CarService(
    IRepository<Car, int> repository,
    IRepository<ModelGeneration, int> generationRepository, 
    IMapper mapper) : IService<CarCreate, CarGet>
{
    public int Create(CarCreate entity_dto)
    {
        ModelGeneration generation = generationRepository.Read(entity_dto.GenerationId)
             ?? throw new KeyNotFoundException($"ModelGeneration(id={entity_dto.GenerationId}) does not exist");

        Car entity = mapper.Map<Car>(entity_dto);
        entity.Generation = generation;

        repository.Create(entity);
        return entity.Id;
    }

    public void Update(CarCreate entity_dto)
    {
        ModelGeneration generation = generationRepository.Read(entity_dto.GenerationId)
             ?? throw new KeyNotFoundException($"ModelGeneration(id={entity_dto.GenerationId}) does not exist");

        Car entity = mapper.Map<Car>(entity_dto);
        entity.Generation = generation;

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
