namespace CarRental.Domain.Entities;


public class Car
{
    public required int Id { get; set; }
    public required ModelGeneration Generation { get; set; }
    public required string LicensePlate {  get; set; }
    public required string color { get; set; }
}