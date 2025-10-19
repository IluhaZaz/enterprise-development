using CarRental.Domain.Enums;

namespace CarRental.Domain.Entities;


public class CarModel
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required int SeatsNumber { get; set; }
    public required CarDriveType DriveType { get; set; }
    public required CarBodyType BodyType { get; set; }
    public required CarClass Class { get; set; }
}