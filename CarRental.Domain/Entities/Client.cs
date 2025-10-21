namespace CarRental.Domain.Entities;


public class Client
{
    public required int Id { get; set; }
    public required string LastName { get; set; }
    public required string FirstName { get; set; }
    public required string Patronymic { get; set; }
    public required DateTime BirthDate { get; set; }
    public required string DriverLicense { get; set; }
}