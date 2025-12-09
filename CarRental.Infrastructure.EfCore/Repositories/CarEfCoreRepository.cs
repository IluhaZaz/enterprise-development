using CarRental.Domain.Entities;
using CarRental.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Infrastructure.EfCore.Repositories;

public class CarEfCoreRepository(CarRentalDbContext context) : IRepository<Car, int>
{
    public async Task<int> Create(Car entity)
    {
        await context.Cars.AddAsync(entity);
        await context.SaveChangesAsync();
        return entity.Id;
    }

    public async Task<bool> Delete(int entityId)
    {
        var entity = await context.Cars.FirstOrDefaultAsync(e => e.Id == entityId);
        if (entity == null)
            return false;

        context.Cars.Remove(entity);
        await context.SaveChangesAsync();

        return true;
    }

    public async Task<Car?> Read(int entityId) =>
        await context.Cars
            .Include(c => c.Generation)
                .ThenInclude(g => g.Model)
            .FirstOrDefaultAsync(e => e.Id == entityId);

    public async Task<List<Car>> ReadAll() =>
        await context.Cars
            .Include(c => c.Generation)
                .ThenInclude(g => g.Model)
            .ToListAsync();


    public async Task Update(Car entity)
    {
        context.Cars.Update(entity);

        await context.SaveChangesAsync();
    }
}