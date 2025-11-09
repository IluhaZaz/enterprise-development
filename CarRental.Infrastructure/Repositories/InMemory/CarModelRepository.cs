using CarRental.Domain.Entities;
using CarRental.Domain.Interfaces;

namespace CarRental.Infrastructure.Repositories.InMemory;

public class CarModelRepository : IRepository<CarModel, int>
{
    private readonly List<CarModel> _carModels;
    private int _currId;

    public CarModelRepository()
    {
        _carModels = new List<CarModel>();
        _currId = 1;
    }

    public int Create(CarModel entity)
    {
        entity.Id = _currId;
        _carModels.Add(entity);
        _currId++;

        return entity.Id;
    }

    public void Update(CarModel entity)
    {
        Delete(entity.Id);
        _carModels.Add(entity);
    }

    public bool Delete(int id)
    {
        CarModel? carModel = Read(id);
        if (carModel != null)
        {
            _carModels.Remove(carModel);
            return true;
        }
        return false;
    }

    public List<CarModel> ReadAll()
    {
        return [.. _carModels];
    }

    public CarModel? Read(int id)
    {
        return _carModels.FirstOrDefault(c => c.Id == id);
    }
}
