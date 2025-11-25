namespace CarRental.Application.Contracts;

using System.Drawing;

/// <summary>
/// DTO for creating or updating Car object
/// </summary>
public class CarCreate
{
    /// <summary>
    /// Reference to car's model generation
    /// </summary>
    public required int GenerationId { get; set; }

    /// <summary>
    /// Number of license plate installed on car
    /// </summary>
    public required string LicensePlate { get; set; }

    /// <summary>
    /// Car's color
    /// </summary>
    public Color? Color { get; set; }
}
