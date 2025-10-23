using CarRental.Domain.Enums;

namespace CarRental.Domain.Entities;

/// <summary>
/// Contains characteristics for specific model's generation
/// </summary>
public class ModelGeneration
{
    /// <summary>
    /// Unique identifier for model's generation
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Íear of model's release
    /// </summary>
    public required int Year { get; set; }

    /// <summary>
    /// Engine's volume, liters
    /// </summary>
    public required float EngineVolume { get; set; }

    /// <summary>
    /// Reference to car's model
    /// </summary>
    public required CarModel Model { get; set; }

    /// <summary>
    /// Type of model's transmission  type
    /// </summary>
    public required CarTransmissionType TransmissionType { get; set; }

    /// <summary>
    /// Price for rent, rubles per hour
    /// </summary>
    public required decimal PricePerHour { get; set; } 
}