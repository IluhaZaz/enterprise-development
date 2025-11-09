using CarRental.Domain.Entities;
using CarRental.Domain.Interfaces;

namespace CarRental.Infrastructure.Repositories.InMemory;

public class CarRepository: IRepository<Car, int>
{
    private readonly List<Car> _cars;
    private int _currId;

    public CarRepository()
    {
        _cars = new List<Car>();
        _currId = 1;
    }

    public int Create(Car entity)
    {   
        entity.Id = _currId;
        _cars.Add(entity);
        _currId++;

        return entity.Id;
    }

    public void Update(Car entity)
    {
        Delete(entity.Id);
        _cars.Add(entity);
    }

    public bool Delete(int id)
    {
        Car? car = Read(id);
        if (car != null)
        {
            _cars.Remove(car);
            return true;
        }
        return false;
    }

    public List<Car> ReadAll()
    {
        return [.. _cars];
    }

    public Car? Read(int id)
    {
        return _cars.FirstOrDefault(c => c.Id == id);
    }
}
