namespace CarRental.Tests;

public class CarRenatalTests(CarRentalFixture fixture) : IClassFixture<CarRentalFixture>
{
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
}