using Bogus;
using CarRental.Application.Contracts;
using Microsoft.Extensions.Options;

namespace CarRental.Generator;

/// <summary>
/// Generates random rental log data using Bogus library
/// </summary>
public class RentalLogGenerator(IOptions<GeneratorOptions> options)
{
    private readonly Faker<RentalLogCreate> _faker = new Faker<RentalLogCreate>()
        .RuleFor(r => r.CarId, f => f.Random.Int(1, options.Value.MaxCarId))
        .RuleFor(r => r.ClientId, f => f.Random.Int(1, options.Value.MaxClientId))
        .RuleFor(r => r.RentStartDate, f => f.Date.Between(DateTime.UtcNow.AddDays(-30), DateTime.UtcNow.AddDays(30)))
        .RuleFor(r => r.Duration, f => f.Random.Double(1, 30));

    /// <summary>
    /// Generates a batch of random rental log entries
    /// </summary>
    /// <param name="count">Number of entries to generate</param>
    /// <returns>Collection of randomly generated <see cref="RentalLogCreate"/> objects</returns>
    public IEnumerable<RentalLogCreate> Generate(int count) => _faker.Generate(count);
}