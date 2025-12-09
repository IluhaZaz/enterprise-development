using CarRental.Domain.Entities;
using CarRental.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Infrastructure.EfCore.Repositories;

/// <summary>
/// EF Core repository for Car entity with CRUD operations and eager loading of Generation and Model
/// </summary>
public class CarEfCoreRepository(CarRentalDbContext context) : IRepository<Car, int>
{
    /// <summary>
    /// Create new car entity and return its id
    /// </summary>
    public async Task<int> Create(Car entity)
    {
        await context.Cars.AddAsync(entity);
        await context.SaveChangesAsync();
        return entity.Id;
    }

    /// <summary>
    /// Delete car entity by id and return operation result
    /// </summary>
    public async Task<bool> Delete(int entityId)
    {
        var entity = await context.Cars.FirstOrDefaultAsync(e => e.Id == entityId);
        if (entity == null)
            return false;

        context.Cars.Remove(entity);
        await context.SaveChangesAsync();

        return true;
    }

    /// <summary>
    /// Read car entity by id with related Generation and Model
    /// </summary>
    public async Task<Car?> Read(int entityId) =>
        await context.Cars
            .Include(c => c.Generation)
                .ThenInclude(g => g!.Model)
            .FirstOrDefaultAsync(e => e.Id == entityId);

    /// <summary>
    /// Read all car entities with related Generation and Model
    /// </summary>
    public async Task<List<Car>> ReadAll() =>
        await context.Cars
            .Include(c => c.Generation)
                .ThenInclude(g => g!.Model)
            .ToListAsync();

    /// <summary>
    /// Update car entity data
    /// </summary>
    public async Task Update(Car entity)
    {
        context.Cars.Update(entity);

        await context.SaveChangesAsync();
    }
}