namespace CarRental.Application.Contracts;

/// <summary>
/// Represents a car with the number of times it has been rented
/// </summary>
public class CarRentCountStat
{
    /// <summary>
    /// Car data
    /// </summary>
    public required CarGet Car { get; set; }

    /// <summary>
    /// Number of rentals for this car
    /// </summary>
    public required int RentCount { get; set; }
}