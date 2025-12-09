using CarRental.Domain.Entities;
using CarRental.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Infrastructure.EfCore.Repositories;

public class ModelGenerationEfCoreRepository(CarRentalDbContext context) : IRepository<ModelGeneration, int>
{
    public async Task<int> Create(ModelGeneration entity)
    {
        await context.ModelGenerations.AddAsync(entity);
        await context.SaveChangesAsync();
        return entity.Id;
    }

    public async Task<bool> Delete(int entityId)
    {
        var entity = await context.ModelGenerations.FirstOrDefaultAsync(e => e.Id == entityId);
        if (entity == null)
            return false;

        context.ModelGenerations.Remove(entity);
        await context.SaveChangesAsync();

        return true;
    }

    public async Task<ModelGeneration?> Read(int entityId) =>
        await context.ModelGenerations
            .Include(g => g.Model)
            .FirstOrDefaultAsync(e => e.Id == entityId);

    public async Task<List<ModelGeneration>> ReadAll() =>
        await context.ModelGenerations
            .Include(g => g.Model)
            .ToListAsync();

    public async Task Update(ModelGeneration entity)
    {
        context.ModelGenerations.Update(entity);
        await context.SaveChangesAsync();
    }
}