namespace CarRental.Domain.Entities;

/// <summary>
/// Stores all rent history
/// </summary>
public class RentalLog
{
    /// <summary>
    /// Unique identifier for rent contract
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Reference to rented car
    /// </summary>
    public required Car Car { get; set; }

    /// <summary>
    /// Reference to person who rented car
    /// </summary>
    public required Client Client { get; set; }

    /// <summary>
    /// Start date of rent
    /// </summary>
    public required DateTime RentStartDate { get; set; }

    /// <summary>
    /// How much rent lasts
    /// </summary>
    public required Decimal Duration { get; set; }
}