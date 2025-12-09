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
    /// <summary>
    /// Create new entity instance and return it's ID
    /// </summary>
    public async Task<int> Create(RentalLogCreate entity_dto)
    {
        Car car = await carRepository.Read(entity_dto.CarId)
            ?? throw new KeyNotFoundException($"Car(id={entity_dto.CarId}) does not exist");
        Client client = await clientRepository.Read(entity_dto.ClientId)
            ?? throw new KeyNotFoundException($"Client(id={entity_dto.ClientId}) does not exist");

        RentalLog entity = mapper.Map<RentalLog>(entity_dto);
        entity.Car = car;
        entity.Client = client;

        await repository.Create(entity);
        return entity.Id;
    }

    /// <summary>
    /// Update entity's data
    /// </summary>
    public async Task Update(RentalLogCreate entity_dto, int rental_id)
    {
        Car car = await carRepository.Read(entity_dto.CarId)
            ?? throw new KeyNotFoundException($"Car(id={entity_dto.CarId}) does not exist");
        Client client = await clientRepository.Read(entity_dto.ClientId)
            ?? throw new KeyNotFoundException($"Client(id={entity_dto.ClientId}) does not exist");

        var existing = await repository.Read(rental_id)
            ?? throw new KeyNotFoundException($"RentalLog(id={rental_id}) does not exist");

        mapper.Map(entity_dto, existing);
        existing.Car = car;
        existing.Client = client;

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
    public async Task<List<RentalLogGet>> ReadAll()
    {
        List<RentalLog> res = await repository.ReadAll();
        return mapper.Map<List<RentalLogGet>>(res);
    }

    /// <summary>
    /// Return entity from storage by id
    /// </summary>
    public async Task<RentalLogGet?> Read(int id)
    {
        RentalLog? entity = await repository.Read(id);
        return mapper.Map<RentalLogGet>(entity);
    }

    /// <summary>
    /// Return linked Car's DTO
    /// </summary>
    public async Task<CarGet?> GetCar(int logId)
    {
        RentalLog? log = await repository.Read(logId);
        if (log != null)
            return mapper.Map<CarGet>(log.Car);
        return null;
    }

    /// <summary>
    /// Return linked Clients's DTO
    /// </summary>
    public async Task<ClientGet?> GetClient(int logId)
    {
        RentalLog? log = await repository.Read(logId);
        if (log != null)
            return mapper.Map<ClientGet>(log.Client);
        return null;
    }
}
