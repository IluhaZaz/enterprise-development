using CarRental.Domain.Entities;
using CarRental.Domain.Interfaces;

namespace CarRental.Infrastructure.Repositories.InMemory;

public class RentalLogRepository : IRepository<RentalLog, int>
{
    private readonly List<RentalLog> _rentalLogs;
    private int _currId;

    public RentalLogRepository()
    {
        _rentalLogs = new List<RentalLog>();
        _currId = 1;
    }

    public int Create(RentalLog entity)
    {
        entity.Id = _currId;
        _rentalLogs.Add(entity);
        _currId++;

        return entity.Id;
    }

    public void Update(RentalLog entity)
    {
        Delete(entity.Id);
        _rentalLogs.Add(entity);
    }

    public bool Delete(int id)
    {
        RentalLog? rentalLog = Read(id);
        if (rentalLog != null)
        {
            _rentalLogs.Remove(rentalLog);
            return true;
        }
        return false;
    }

    public List<RentalLog> ReadAll()
    {
        return [.. _rentalLogs];
    }

    public RentalLog? Read(int id)
    {
        return _rentalLogs.FirstOrDefault(c => c.Id == id);
    }
}
