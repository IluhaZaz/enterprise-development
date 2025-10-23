using CarRental.Domain.Enums;

namespace CarRental.Domain.Entities;

/// <summary>
/// Contains car model's characteristics
/// </summary>
public class CarModel
{
    /// <summary>
    /// Unique identifier for car's model
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Car model's name
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Count of seats in car
    /// </summary>
    public required int SeatsNumber { get; set; }

    /// <summary>
    /// Type of model's drive system
    /// </summary>
    public required CarDriveType DriveType { get; set; }

    /// <summary>
    /// Type of model's body
    /// </summary>
    public required CarBodyType BodyType { get; set; }

    /// <summary>
    /// Model's class by prestige
    /// </summary>
    public required CarClass Class { get; set; }
}