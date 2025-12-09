using CarRental.Tests.Fixtures;

namespace CarRental.Tests;


/// <summary>
/// Class containing unit tests to check domain classes work properly
/// </summary>
public class CarRenatalRepositoryTests(RepositoryFixture fixture) : IClassFixture<RepositoryFixture>
{
    /// <summary>
    /// Retrieves all clients who have rented cars of a specified model, 
    /// ordered alphabetically by last name, first name, and patronymic
    /// </summary>
    [Fact]
    public async Task GetClientsRentedModelAsync()
    {
        var cars = await fixture.CarModelRepository.ReadAll();
        var target = cars[1];

        var expectedClientsId = new[] { 2, 6, 7 };

        var logs = await fixture.RentalLogRepository.ReadAll();
        var actual = logs
            .Where(r => r.Car.Generation.Model.Name == target.Name)
            .Select(r => r.Client)
            .Distinct()
            .OrderBy(c => c.LastName)
            .ThenBy(c => c.FirstName)
            .ThenBy(c => c.Patronymic)
            .ToList()
            .Select(c => c.Id);
        Assert.Equal(expectedClientsId, actual);
    }


    /// <summary>
    /// Retrieves all cars that are currently rented
    /// </summary>
    [Fact]
    public async void GetCarsInRent()
    {
        var currentTime = new DateTime(2024, 2, 19, 14, 0, 0);

        var expectedCarsId = new[] { 1, 2 };

        var logs = await fixture.RentalLogRepository.ReadAll();
        var actual = logs
            .Where(r => r.RentStartDate <= currentTime && currentTime <= r.RentStartDate.AddHours((double)r.Duration))
            .Select(r => r.Car.Id);
        Assert.Equal(expectedCarsId, actual);
    }

    /// <summary>
    /// Retrieves the top 5 most frequently rented cars based on total rental count
    /// </summary>
    [Fact]
    public async void GetTopFiveCars()
    {
        var expectedCarsId = new[] { 1, 2, 4, 6, 9 };

        var logs = await fixture.RentalLogRepository.ReadAll();
        var actual = logs
            .GroupBy(log => log.Car)
            .Select(g => new { Car = g.Key, Count = g.Count() })
            .OrderByDescending(x => x.Count)
            .Take(5)
            .Select(x => x.Car.Id)
            .ToList();
        Assert.Equal(expectedCarsId, actual);
    }

    /// <summary>
    /// Returns the total number of rentals for each car
    /// </summary>
    [Fact]
    public async void GetRentNumByCar()
    {
        var cars = await fixture.CarRepository.ReadAll();
        var allCars = cars
            .OrderBy(c => c.Id)
            .ToList();
        var expected = new[] { 4, 3, 1, 3, 1, 2, 1, 1, 2, 1, 2, 1, 1, 1 };

        var carsLen = allCars.Count;

        var expectedResult = new Dictionary<int, int>();
        for (var i = 0; i < carsLen; i++)
        {
            expectedResult[i + 1] = expected[i];
        }

        var logs = await fixture.RentalLogRepository.ReadAll();
        var actual = logs
            .GroupBy(r => r.Car)
            .Select(g => new { Car = g.Key, Count = g.Count() })
            .OrderBy(c => c.Car.Id)
            .ToDictionary(g => g.Car.Id, g => g.Count);

        Assert.Equal(expectedResult, actual);
    }

    /// <summary>
    /// Retrieves the top 5 clients with the highest total rental amount, 
    /// calculated as the sum of (duration × price per hour) across all their rentals
    /// </summary>
    [Fact]
    public async void GetTopFiveClientsByRent()
    {
        var expectedClientsId = new[] { 6, 5, 2, 1, 4 };

        var logs = await fixture.RentalLogRepository.ReadAll();
        var actual = logs
        .GroupBy(r => r.Client)
        .Select(g => new
        {
            Client = g.Key,
            TotalAmount = g.Sum(r => (decimal)r.Duration * r.Car.Generation.PricePerHour)
        })
        .OrderByDescending(x => x.TotalAmount)
        .Take(5)
        .Select(c => c.Client.Id)
        .ToList();

        Assert.Equal(actual, expectedClientsId);
    }
}