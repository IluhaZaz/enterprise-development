using CarRental.Domain.Entities;
using CarRental.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Infrastructure.EfCore.Repositories;

/// <summary>
/// EF Core repository for ModelGeneration entity with CRUD operations and eager loading of Model
/// </summary>
public class ModelGenerationEfCoreRepository(CarRentalDbContext context) : IRepository<ModelGeneration, int>
{
    /// <summary>
    /// Create new model generation entity and return its id
    /// </summary>
    public async Task<int> Create(ModelGeneration entity)
    {
        await context.ModelGenerations.AddAsync(entity);
        await context.SaveChangesAsync();
        return entity.Id;
    }

    /// <summary>
    /// Delete model generation entity by id and return operation result
    /// </summary>
    public async Task<bool> Delete(int entityId)
    {
        var entity = await context.ModelGenerations.FirstOrDefaultAsync(e => e.Id == entityId);
        if (entity == null)
            return false;

        context.ModelGenerations.Remove(entity);
        await context.SaveChangesAsync();

        return true;
    }

    /// <summary>
    /// Read model generation entity by id with related Model
    /// </summary>
    public async Task<ModelGeneration?> Read(int entityId) =>
        await context.ModelGenerations
            .Include(g => g.Model)
            .FirstOrDefaultAsync(e => e.Id == entityId);

    /// <summary>
    /// Read all model generation entities with related Model
    /// </summary>
    public async Task<List<ModelGeneration>> ReadAll() =>
        await context.ModelGenerations
            .Include(g => g.Model)
            .ToListAsync();

    /// <summary>
    /// Update model generation entity data
    /// </summary>
    public async Task Update(ModelGeneration entity)
    {
        context.ModelGenerations.Update(entity);

        await context.SaveChangesAsync();
    }
}