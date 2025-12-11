using CarRental.Application.Contracts;

namespace CarRental.Application.Interfaces;

/// <summary>
/// Provides analytical operations based on rental data
/// </summary>
public interface IAnalyticsService
{
    /// <summary>
    /// Returns all clients who rented cars of the specified model with rent counts ordered by full name
    /// </summary>
    public Task<IList<ClientModelRentStat>> GetClientsByCarModel(int modelId);

    /// <summary>
    /// Returns all cars that are currently in rent at the specified moment
    /// </summary>
    public Task<IList<CarGet>> GetCarsInRent(DateTime currentTime);

    /// <summary>
    /// Returns the top 5 most frequently rented cars
    /// </summary>
    public Task<IList<CarGet>> GetTopFiveCars();

    /// <summary>
    /// Returns rental count summary for each car
    /// </summary>
    public Task<IList<CarRentCountStat>> GetRentCountPerCar();

    /// <summary>
    /// Returns the top 5 clients by total rental cost
    /// </summary>
    public Task<IList<ClientRentAmountStat>> GetTopFiveClientsByRent();
}