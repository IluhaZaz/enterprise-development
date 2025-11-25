using CarRental.Domain.Enums;

namespace CarRental.Application.Contracts;


/// <summary>
/// DTO for getting ModelGeneration object
/// </summary>
public class ModelGenerationGet
{
    /// <summary>
    /// Unique identifier for model's generation
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Year of model's release
    /// </summary>
    public int? Year { get; set; }

    /// <summary>
    /// Engine's volume, liters
    /// </summary>
    public double? EngineVolume { get; set; }

    /// <summary>
    /// Reference to car's model
    /// </summary>
    public required int ModelId { get; set; }

    /// <summary>
    /// Type of model's transmission  type
    /// </summary>
    public CarTransmissionType? TransmissionType { get; set; }

    /// <summary>
    /// Price for rent, rubles per hour
    /// </summary>
    public required decimal PricePerHour { get; set; }
}