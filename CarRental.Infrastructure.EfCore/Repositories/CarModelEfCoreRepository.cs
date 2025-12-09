using CarRental.Domain.Entities;
using CarRental.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Infrastructure.EfCore.Repositories;

/// <summary>
/// EF Core repository for CarModel entity with CRUD operations
/// </summary>
public class CarModelEfCoreRepository(CarRentalDbContext context) : IRepository<CarModel, int>
{
    /// <summary>
    /// Create new car model entity and return its id
    /// </summary>
    public async Task<int> Create(CarModel entity)
    {
        await context.CarModels.AddAsync(entity);
        await context.SaveChangesAsync();
        return entity.Id;
    }

    /// <summary>
    /// Delete car model entity by id and return operation result
    /// </summary>
    public async Task<bool> Delete(int entityId)
    {
        var entity = await context.CarModels.FirstOrDefaultAsync(e => e.Id == entityId);
        if (entity == null)
            return false;

        context.CarModels.Remove(entity);
        await context.SaveChangesAsync();

        return true;
    }

    /// <summary>
    /// Read car model entity by id
    /// </summary>
    public async Task<CarModel?> Read(int entityId) =>
        await context.CarModels.FirstOrDefaultAsync(e => e.Id == entityId);

    /// <summary>
    /// Read all car model entities
    /// </summary>
    public async Task<List<CarModel>> ReadAll() =>
        await context.CarModels.ToListAsync();

    /// <summary>
    /// Update car model entity data
    /// </summary>
    public async Task Update(CarModel entity)
    {
        context.CarModels.Update(entity);

        await context.SaveChangesAsync();
    }
}