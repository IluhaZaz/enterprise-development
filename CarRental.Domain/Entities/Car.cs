using System.Drawing;

namespace CarRental.Domain.Entities;

/// <summary>
/// Contains rent car's characteristics
/// </summary>
public class Car
{
    /// <summary>
    /// Unique identifier for car
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Represents car's model generation
    /// </summary>
    public required ModelGeneration Generation { get; set; }

    /// <summary>
    /// Number of license plate installed on car
    /// </summary>
    public required string LicensePlate {  get; set; }

    /// <summary>
    /// Car's color
    /// </summary>
    public Color? Color { get; set; }
}