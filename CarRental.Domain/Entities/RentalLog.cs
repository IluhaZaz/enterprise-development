namespace CarRental.Domain.Entities;

public class RentalLog
{
	public required int Id { get; set; }
	public required Car Car { get; set; }
	public required Client Client { get; set; }
	public required DateTime RentStartDate { get; set; }
	public required Decimal Duration { get; set; }
}