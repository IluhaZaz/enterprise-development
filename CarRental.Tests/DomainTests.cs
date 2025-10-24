using CarRental.Domain.Entities;
using CarRental.Domain.TestData;

namespace CarRental.Tests;


/// <summary>
/// Class containing unit tests to check domain classes work properly
/// </summary>
public class CarRenatalTests(CarRentalDataSeed fixture) : IClassFixture<CarRentalDataSeed>
{
    /// <summary>
    /// Retrieves all clients who have rented cars of a specified model, 
    /// ordered alphabetically by last name, first name, and patronymic
    /// </summary>
    [Fact]
    public void GetClientsRentedModel()
    {
        var target = fixture.CarModels[1];

        var expectedClientsId = new[] { 2, 6, 7};
        var expectedClients = fixture.Clients
            .Where(c => expectedClientsId.Contains(c.Id))
            .OrderBy(c => c.LastName)
            .ThenBy(c => c.FirstName)
            .ThenBy(c => c.Patronymic)
            .ToList();

        var actual = fixture.RentalLogs
            .Where(r => r.Car.Generation.Model.Name == target.Name)
            .Select(r => r.Client)
            .Distinct()
            .OrderBy(c => c.LastName)
            .ThenBy(c => c.FirstName)
            .ThenBy(c => c.Patronymic)
            .ToList();
        Assert.Equal(expectedClients, actual);
    }


    /// <summary>
    /// Retrieves all cars that are currently rented
    /// </summary>
    [Fact]
    public void GetCarsInRent()
    {
        var currentTime = new DateTime(2024, 2, 19, 14, 0, 0);

        var expectedCarsId = new[] { 1, 2 };
        var expectedCars = fixture.Cars
            .Where(c => expectedCarsId.Contains(c.Id))
            .ToList();

        var actual = fixture.RentalLogs
            .Where(r => r.RentStartDate <= currentTime && currentTime <= r.RentStartDate.AddHours((double)r.Duration))
            .Select(r => r.Car);
        Assert.Equal(expectedCars, actual);
    }

    /// <summary>
    /// Retrieves the top 5 most frequently rented cars based on total rental count
    /// </summary>
    [Fact]
    public void GetTopFiveCars()
    {
        var expectedCarsId = new[] { 1, 2, 4, 6, 9 };
        var expectedCars = fixture.Cars
            .Where(c => expectedCarsId.Contains(c.Id))
            .ToList();

        var actual = fixture.RentalLogs
            .GroupBy(log => log.Car)
            .Select(g => new { Car = g.Key, Count = g.Count() })
            .OrderByDescending(x => x.Count)
            .Take(5)
            .Select(x => x.Car)
            .ToList();
        Assert.Equal(expectedCars, actual);
    }

    /// <summary>
    /// Returns the total number of rentals for each car
    /// </summary>
    [Fact]
    public void GetRentNumByCar()
    {
        var allCars = fixture.Cars
            .OrderBy(c => c.Id)
            .ToList();
        var expected = new[] { 4, 3, 1, 3, 1, 2, 1, 1, 2, 1, 2, 1, 1, 1 };

        var carsLen = allCars.Count;

        var expectedResult = new Dictionary<Car, int>();
        for (var i = 0; i < carsLen; i++)
        {
            expectedResult[allCars[i]] = expected[i];
        }

        var actual = fixture.RentalLogs
            .GroupBy(r => r.Car)
            .Select(g => new { Car = g.Key, Count = g.Count() })
            .OrderBy(c => c.Car.Id)
            .ToDictionary(g => g.Car, g => g.Count);

        Assert.Equal(expectedResult, actual);
    }

    /// <summary>
    /// Retrieves the top 5 clients with the highest total rental amount, 
    /// calculated as the sum of (duration × price per hour) across all their rentals
    /// </summary>
    [Fact]
    public void GetTopFiveClientsByRent()
    {
        var expectedClientsId = new[] { 6, 5, 2, 1, 4 };
        var expectedClients = fixture.Clients
            .Where(c => expectedClientsId.Contains(c.Id))
            .OrderBy(c => c.Id)
            .ToList();

        var actual = fixture.RentalLogs
        .GroupBy(r => r.Client)
        .Select(g => new
        {
            Client = g.Key,
            TotalAmount = g.Sum(r => (decimal)r.Duration * r.Car.Generation.PricePerHour)
        })
        .OrderByDescending(x => x.TotalAmount)
        .Take(5)
        .Select(c => c.Client)
        .OrderBy(c => c.Id)
        .ToList();

        Assert.Equal(actual, expectedClients);
    }
}