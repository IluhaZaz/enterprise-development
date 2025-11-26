using AutoMapper;
using CarRental.Application.Contracts;
using CarRental.Application.Interfaces;
using CarRental.Domain.Entities;
using CarRental.Domain.Interfaces;

namespace CarRental.Application.Services;
/// <summary>
/// Provide CRUD operation for RentalLog entity
/// </summary>
public class RentalLogService(
    IRepository<RentalLog, int> repository,
    IRepository<Car, int> carRepository,
    IRepository<Client, int> clientRepository,
    IMapper mapper) : IService<RentalLogCreate, RentalLogGet>
{
    // <summary>
    /// Create new entity instance and return it's ID
    /// </summary>
    public int Create(RentalLogCreate entity_dto)
    {
        Car car = carRepository.Read(entity_dto.CarId)
             ?? throw new KeyNotFoundException($"Car(id={entity_dto.CarId}) does not exist");
        Client client = clientRepository.Read(entity_dto.ClientId)
             ?? throw new KeyNotFoundException($"Client(id={entity_dto.ClientId}) does not exist");

        RentalLog entity = mapper.Map<RentalLog>(entity_dto);
        entity.Car = car;
        entity.Client = client;

        repository.Create(entity);
        return entity.Id;
    }

    /// <summary>
    /// Update entity's data
    /// </summary>
    public void Update(RentalLogCreate entity_dto)
    {
        Car car = carRepository.Read(entity_dto.CarId)
             ?? throw new KeyNotFoundException($"Car(id={entity_dto.CarId}) does not exist");
        Client client = clientRepository.Read(entity_dto.ClientId)
             ?? throw new KeyNotFoundException($"Client(id={entity_dto.ClientId}) does not exist");

        RentalLog entity = mapper.Map<RentalLog>(entity_dto);
        entity.Car = car;
        entity.Client = client;

        repository.Update(entity);
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
    public List<RentalLogGet> ReadAll()
    {
        List<RentalLog> res = repository.ReadAll();
        return mapper.Map<List<RentalLogGet>>(res);
    }

    /// <summary>
    /// Return entity from storage by id
    /// </summary>
    public RentalLogGet? Read(int id)
    {
        RentalLog? entity = repository.Read(id);
        return mapper.Map<RentalLogGet>(entity);
    }

    /// <summary>
    /// Return linked Car's DTO
    /// </summary>
    public CarGet? GetCar(int logId)
    {
        RentalLog? log = repository.Read(logId);
        if (log != null)
            return mapper.Map<CarGet>(log.Car);
        return null;
    }

    /// <summary>
    /// Return linked Clients's DTO
    /// </summary>
    public ClientGet? GetClient(int logId)
    {
        RentalLog? log = repository.Read(logId);
        if (log != null)
            return mapper.Map<ClientGet>(log.Client);
        return null;
    }
}
