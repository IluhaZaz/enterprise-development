using CarRental.Domain.Entities;
using CarRental.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Infrastructure.EfCore.Repositories;

/// <summary>
/// EF Core repository for RentalLog entity with CRUD operations and eager loading of Car Generation Model and Client
/// </summary>
public class RentalLogEfCoreRepository(CarRentalDbContext context) : IRepository<RentalLog, int>
{
    /// <summary>
    /// Create new rental log entity and return its id
    /// </summary>
    public async Task<int> Create(RentalLog entity)
    {
        await context.RentalLogs.AddAsync(entity);
        await context.SaveChangesAsync();
        return entity.Id;
    }

    /// <summary>
    /// Delete rental log entity by id and return operation result
    /// </summary>
    public async Task<bool> Delete(int entityId)
    {
        var entity = await context.RentalLogs.FirstOrDefaultAsync(e => e.Id == entityId);
        if (entity == null)
            return false;

        context.RentalLogs.Remove(entity);
        await context.SaveChangesAsync();

        return true;
    }

    /// <summary>
    /// Read rental log entity by id with related Car Generation Model and Client
    /// </summary>
    public async Task<RentalLog?> Read(int entityId) =>
        await context.RentalLogs
            .Include(l => l.Car)
                .ThenInclude(c => c!.Generation)
                    .ThenInclude(g => g!.Model)
            .Include(l => l.Client)
            .FirstOrDefaultAsync(e => e.Id == entityId);

    /// <summary>
    /// Read all rental log entities with related Car Generation Model and Client
    /// </summary>
    public async Task<List<RentalLog>> ReadAll() =>
        await context.RentalLogs
            .Include(l => l.Car)
                .ThenInclude(c => c!.Generation)
                    .ThenInclude(g => g!.Model)
            .Include(l => l.Client)
            .ToListAsync();

    /// <summary>
    /// Update rental log entity data
    /// </summary>
    public async Task Update(RentalLog entity)
    {
        context.RentalLogs.Update(entity);

        await context.SaveChangesAsync();
    }
}