using AutoMapper;
using CarRental.Application.Contracts;
using CarRental.Application.Interfaces;
using CarRental.Domain.Entities;
using CarRental.Domain.Interfaces;

namespace CarRental.Application.Services;
/// <summary>
/// Provide CRUD operation for Car entity
/// </summary>
public class CarService(
    IRepository<Car, int> repository,
    IRepository<ModelGeneration, int> generationRepository,
    IMapper mapper) : IService<CarCreate, CarGet>
{
    // <summary>
    /// Create new entity instance and return it's ID
    /// </summary>
    public int Create(CarCreate entity_dto)
    {
        ModelGeneration generation = generationRepository.Read(entity_dto.GenerationId)
             ?? throw new KeyNotFoundException($"ModelGeneration(id={entity_dto.GenerationId}) does not exist");

        Car entity = mapper.Map<Car>(entity_dto);
        entity.Generation = generation;

        repository.Create(entity);
        return entity.Id;
    }

    /// <summary>
    /// Update entity's data
    /// </summary>
    public void Update(CarCreate entity_dto, int car_id)
    {
        ModelGeneration generation = generationRepository.Read(entity_dto.GenerationId)
             ?? throw new KeyNotFoundException($"ModelGeneration(id={entity_dto.GenerationId}) does not exist");

        var existing = repository.Read(car_id)
            ?? throw new KeyNotFoundException($"Car(id={car_id}) does not exist");

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
    public List<CarGet> ReadAll()
    {
        List<Car> res = repository.ReadAll();
        return mapper.Map<List<CarGet>>(res);
    }

    /// <summary>
    /// Return entity from storage by id
    /// </summary>
    public CarGet? Read(int id)
    {
        Car? entity = repository.Read(id);
        return mapper.Map<CarGet>(entity);
    }

    /// <summary>
    /// Return linked ModelGenerations's DTO
    /// </summary>
    public ModelGenerationGet? GetModelGeneration(int carId)
    {
        Car? car = repository.Read(carId);
        if (car != null)
            return mapper.Map<ModelGenerationGet>(car.Generation);
        return null;
    }
}
