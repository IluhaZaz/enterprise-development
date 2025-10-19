using CarRental.Domain.Enums;

namespace CarRental.Domain.Entities;

public class ModelGeneration
{
	public required int Id { get; set; }
	public required int Year { get; set; }
	public required float EngineVolume { get; set; }
	public required CarModel Model { get; set; }
	public required CarTransmissionType TransmissionType { get; set; }
	public required decimal PricePerHour { get; set; } 
}