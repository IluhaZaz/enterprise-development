namespace CarRental.Application.Contracts;

/// <summary>
/// Represents a client along with the total rental amount spent
/// </summary>
public class ClientRentAmountStat
{
    /// <summary>
    /// Client data
    /// </summary>
    public required ClientGet Client { get; set; }

    /// <summary>
    /// Total sum of all rentals performed by this client
    /// </summary>
    public required decimal TotalAmount { get; set; }

    /// <summary>
    /// Total number of rentals performed by this client
    /// </summary>
    public required int RentCount { get; set; }
}