using AutoMapper;
using CarRental.Application.Contracts;
using CarRental.Application.Interfaces;
using CarRental.Domain.Entities;
using CarRental.Domain.Interfaces;

namespace CarRental.Application.Services;
public class RentalLogService(
    IRepository<RentalLog, int> repository,
    IRepository<Car, int> carRepository,
    IRepository<Client, int> clientRepository, 
    IMapper mapper) : IService<RentalLogCreate, RentalLogGet>
{
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
