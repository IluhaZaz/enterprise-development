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
    /// FK to rented car
    /// </summary>
    public required int CarId { get; set; }

    /// <summary>
    /// Reference to rented car
    /// </summary>
    public Car? Car { get; set; }

    /// <summary>
    /// FK to client
    /// </summary>
    public required int ClientId { get; set; }

    /// <summary>
    /// Reference to person who rented car
    /// </summary>
    public Client? Client { get; set; }

    /// <summary>
    /// Start date of rent
    /// </summary>
    public required DateTime RentStartDate { get; set; }

    /// <summary>
    /// How much rent lasts
    /// </summary>
    public required double Duration { get; set; }
}