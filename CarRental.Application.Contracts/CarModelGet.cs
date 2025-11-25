using CarRental.Domain.Enums;


namespace CarRental.Application.Contracts;


/// <summary>
/// DTO for getting CarModel object
/// </summary>
public class CarModelGet
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
    public CarDriveType? DriveType { get; set; }

    /// <summary>
    /// Type of model's body
    /// </summary>
    public CarBodyType? BodyType { get; set; }

    /// <summary>
    /// Model's class by prestige
    /// </summary>
    public CarClass? Class { get; set; }
}