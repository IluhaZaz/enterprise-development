using CarRental.Domain.DataSeed;
using CarRental.Domain.Interfaces;
using CarRental.Infrastructure.InMemory.Repositories;

namespace CarRental.Tests.Fixtures;

/// <summary>
/// Provides repositories for all entities
/// </summary>
public class RepositoryFixture
{
    public CarModelRepository CarModelRepository { get; }
    public ModelGenerationRepository ModelGenerationRepository { get; }
    public CarRepository CarRepository { get; }
    public ClientRepository ClientRepository { get; }
    public RentalLogRepository RentalLogRepository { get; }

    /// <summary>
    /// Add data to repositories from data seed
    /// </summary>
    public void FillData()
    {
        var data = new CarRentalDataSeed();

        FillRepository(CarModelRepository, data.CarModels);
        FillRepository(ModelGenerationRepository, data.ModelGenerations);
        FillRepository(CarRepository, data.Cars);
        FillRepository(ClientRepository, data.Clients);
        FillRepository(RentalLogRepository, data.RentalLogs);
    }

    /// <summary>
    /// Fixture initializing method
    /// </summary>
    public RepositoryFixture()
    {
        CarModelRepository = new CarModelRepository();
        ModelGenerationRepository = new ModelGenerationRepository();
        CarRepository = new CarRepository();
        ClientRepository = new ClientRepository();
        RentalLogRepository = new RentalLogRepository();

        FillData();
    }

    /// <summary>
    /// Add data to repository
    /// </summary>
    public static void FillRepository<TEntity>(IRepository<TEntity, int> repo, List<TEntity> items)
    where TEntity : class
    {
        foreach (TEntity item in items)
        {
            repo.Create(item);
        }
    }
}
