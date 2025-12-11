using AutoMapper;
using CarRental.Application.Contracts;
using CarRental.Application.Interfaces;
using CarRental.Domain.Entities;
using CarRental.Domain.Interfaces;

namespace CarRental.Application.Services;

/// <summary>
/// Provides analytical operations based on rental data
/// </summary>
public class AnalyticsService(
    IRepository<CarModel, int> modelRepository,
    IRepository<RentalLog, int> rentalRepository,
    IMapper mapper) : IAnalyticsService
{
    /// <summary>
    /// Returns all clients who rented cars of the specified model with rent counts ordered by full name
    /// </summary>
    public async Task<IList<ClientModelRentStat>> GetClientsByCarModel(int modelId)
    {
        var model = await modelRepository.Read(modelId)
            ?? throw new KeyNotFoundException($"CarModel(id={modelId}) does not exist");

        var logs = await rentalRepository.ReadAll();

        var grouped = logs
            .Where(r => r.Car!.Generation!.ModelId == modelId)
            .GroupBy(r => r.Client!)
            .Select(g => new
            {
                Client = g.Key,
                RentCount = g.Count()
            })
            .OrderBy(x => x.Client.LastName)
            .ThenBy(x => x.Client.FirstName)
            .ThenBy(x => x.Client.Patronymic)
            .ToList();

        var result = new List<ClientModelRentStat>();

        foreach (var item in grouped)
        {
            var clientDto = mapper.Map<ClientGet>(item.Client);
            result.Add(new ClientModelRentStat
            {
                Client = clientDto,
                RentCount = item.RentCount
            });
        }

        return result;
    }

    /// <summary>
    /// Returns all cars that are currently in rent at the specified moment
    /// </summary>
    public async Task<IList<CarGet>> GetCarsInRent(DateTime currentTime)
    {
        var logs = await rentalRepository.ReadAll();

        var activeCars = logs
            .Where(r => r.RentStartDate <= currentTime &&
                        currentTime <= r.RentStartDate.AddHours(r.Duration))
            .Select(r => r.Car!)
            .DistinctBy(c => c.Id)
            .ToList();

        return mapper.Map<IList<CarGet>>(activeCars);
    }

    /// <summary>
    /// Returns the top 5 most frequently rented cars
    /// </summary>
    public async Task<IList<CarGet>> GetTopFiveCars()
    {
        var logs = await rentalRepository.ReadAll();

        var topCars = logs
            .GroupBy(r => r.Car!)
            .Select(g => new { Car = g.Key, Count = g.Count() })
            .OrderByDescending(x => x.Count)
            .Take(5)
            .Select(x => x.Car)
            .ToList();

        return mapper.Map<IList<CarGet>>(topCars);
    }

    /// <summary>
    /// Returns rental count summary for each car
    /// </summary>
    public async Task<IList<CarRentCountStat>> GetRentCountPerCar()
    {
        var logs = await rentalRepository.ReadAll();

        var grouped = logs
            .GroupBy(r => r.Car!)
            .Select(g => new
            {
                Car = g.Key,
                RentCount = g.Count()
            })
            .OrderBy(x => x.Car.Id)
            .ToList();

        var result = new List<CarRentCountStat>();

        foreach (var item in grouped)
        {
            var carDto = mapper.Map<CarGet>(item.Car);
            result.Add(new CarRentCountStat
            {
                Car = carDto,
                RentCount = item.RentCount
            });
        }

        return result;
    }

    /// <summary>
    /// Returns the top 5 clients by total rental cost
    /// </summary>
    public async Task<IList<ClientRentAmountStat>> GetTopFiveClientsByRent()
    {
        var logs = await rentalRepository.ReadAll();

        var grouped = logs
            .GroupBy(r => r.Client!)
            .Select(g => new
            {
                Client = g.Key,
                RentCount = g.Count(),
                TotalAmount = g.Sum(r => (decimal)r.Duration * r.Car!.Generation!.PricePerHour)
            })
            .OrderByDescending(x => x.TotalAmount)
            .Take(5)
            .ToList();

        var result = new List<ClientRentAmountStat>();

        foreach (var item in grouped)
        {
            var clientDto = mapper.Map<ClientGet>(item.Client);
            result.Add(new ClientRentAmountStat
            {
                Client = clientDto,
                TotalAmount = item.TotalAmount,
                RentCount = item.RentCount
            });
        }

        return result;
    }
}