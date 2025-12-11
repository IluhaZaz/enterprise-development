namespace CarRental.Application.Contracts;

/// <summary>
/// Represents a client who rented cars of a specific model
/// </summary>
public class ClientModelRentStat
{
    /// <summary>
    /// Client data
    /// </summary>
    public required ClientGet Client { get; set; }

    /// <summary>
    /// Number of rentals of the specified model performed by this client
    /// </summary>
    public required int RentCount { get; set; }
}