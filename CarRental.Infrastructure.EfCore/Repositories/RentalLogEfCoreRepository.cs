using CarRental.Domain.Entities;
using CarRental.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Infrastructure.EfCore.Repositories;

public class RentalLogEfCoreRepository(CarRentalDbContext context) : IRepository<RentalLog, int>
{
    public async Task<int> Create(RentalLog entity)
    {
        await context.RentalLogs.AddAsync(entity);
        await context.SaveChangesAsync();
        return entity.Id;
    }

    public async Task<bool> Delete(int entityId)
    {
        var entity = await context.RentalLogs.FirstOrDefaultAsync(e => e.Id == entityId);
        if (entity == null)
            return false;

        context.RentalLogs.Remove(entity);
        await context.SaveChangesAsync();

        return true;
    }

    public async Task<RentalLog?> Read(int entityId) =>
        await context.RentalLogs
            .Include(l => l.Car)
                .ThenInclude(c => c!.Generation)
                    .ThenInclude(g => g!.Model)
            .Include(l => l.Client)
            .FirstOrDefaultAsync(e => e.Id == entityId);

    public async Task<List<RentalLog>> ReadAll() =>
        await context.RentalLogs
            .Include(l => l.Car)
                .ThenInclude(c => c!.Generation)
                    .ThenInclude(g => g!.Model)
            .Include(l => l.Client)
            .ToListAsync();

    public async Task Update(RentalLog entity)
    {
        context.RentalLogs.Update(entity);
        await context.SaveChangesAsync();
    }
}