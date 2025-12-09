using CarRental.Domain.DataSeed;

namespace CarRental.Tests;

/// <summary>
/// Class containing unit tests to check domain classes work properly
/// </summary>
public class CarRenatalDomainTests(CarRentalDataSeed fixture) : IClassFixture<CarRentalDataSeed>
{
    /// <summary>
    /// Retrieves all clients who have rented cars of a specified model,
    /// ordered alphabetically by last name, first name, and patronymic
    /// </summary>
    [Fact]
    public void GetClientsRentedModel()
    {
        var target = fixture.CarModels[1];
        var expectedClientsId = new[] { 2, 6, 7 };

        var carById = fixture.Cars.ToDictionary(c => c.Id);
        var genById = fixture.ModelGenerations.ToDictionary(g => g.Id);
        var clientById = fixture.Clients.ToDictionary(c => c.Id);

        var actual = fixture.RentalLogs
            .Where(r =>
            {
                var car = carById[r.CarId];
                var gen = genById[car.GenerationId];
                return gen.ModelId == target.Id;
            })
            .Select(r => clientById[r.ClientId])
            .DistinctBy(c => c.Id)
            .OrderBy(c => c.LastName)
            .ThenBy(c => c.FirstName)
            .ThenBy(c => c.Patronymic)
            .Select(c => c.Id);

        Assert.Equal(expectedClientsId, actual);
    }

    /// <summary>
    /// Retrieves all cars that are currently rented
    /// </summary>
    [Fact]
    public void GetCarsInRent()
    {
        var currentTime = new DateTime(2024, 2, 19, 14, 0, 0);
        var expectedCarsId = new[] { 1, 2 };

        var actual = fixture.RentalLogs
            .Where(r => r.RentStartDate <= currentTime && currentTime <= r.RentStartDate.AddHours(r.Duration))
            .Select(r => r.CarId);

        Assert.Equal(expectedCarsId, actual);
    }

    /// <summary>
    /// Retrieves the top 5 most frequently rented cars based on total rental count
    /// </summary>
    [Fact]
    public void GetTopFiveCars()
    {
        var expectedCarsId = new[] { 1, 2, 4, 6, 9 };

        var actual = fixture.RentalLogs
            .GroupBy(log => log.CarId)
            .Select(g => new { CarId = g.Key, Count = g.Count() })
            .OrderByDescending(x => x.Count)
            .ThenBy(x => x.CarId)
            .Take(5)
            .Select(x => x.CarId)
            .ToList();

        Assert.Equal(expectedCarsId, actual);
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

        var expectedResult = new Dictionary<int, int>();
        for (var i = 0; i < allCars.Count; i++)
            expectedResult[i + 1] = expected[i];

        var actual = fixture.RentalLogs
            .GroupBy(r => r.CarId)
            .OrderBy(g => g.Key)
            .ToDictionary(g => g.Key, g => g.Count());

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

        var carById = fixture.Cars.ToDictionary(c => c.Id);
        var genById = fixture.ModelGenerations.ToDictionary(g => g.Id);

        var actual = fixture.RentalLogs
            .GroupBy(r => r.ClientId)
            .Select(g => new
            {
                ClientId = g.Key,
                TotalAmount = g.Sum(r =>
                {
                    var car = carById[r.CarId];
                    var gen = genById[car.GenerationId];
                    return (decimal)r.Duration * gen.PricePerHour;
                })
            })
            .OrderByDescending(x => x.TotalAmount)
            .ThenBy(x => x.ClientId)
            .Take(5)
            .Select(x => x.ClientId)
            .ToList();

        Assert.Equal(expectedClientsId, actual);
    }
}