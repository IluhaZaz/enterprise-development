using CarRental.Domain.Entities;
using CarRental.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Infrastructure.EfCore.Repositories;

public class CarModelEfCoreRepository(CarRentalDbContext context) : IRepository<CarModel, int>
{
    public async Task<int> Create(CarModel entity)
    {
        await context.CarModels.AddAsync(entity);
        await context.SaveChangesAsync();
        return entity.Id;
    }

    public async Task<bool> Delete(int entityId)
    {
        var entity = await context.CarModels.FirstOrDefaultAsync(e => e.Id == entityId);
        if (entity == null)
            return false;

        context.CarModels.Remove(entity);
        await context.SaveChangesAsync();

        return true;
    }

    public async Task<CarModel?> Read(int entityId) =>
        await context.CarModels.FirstOrDefaultAsync(e => e.Id == entityId);

    public async Task<List<CarModel>> ReadAll() =>
        await context.CarModels.ToListAsync();

    public async Task Update(CarModel entity)
    {
        context.CarModels.Update(entity);
        await context.SaveChangesAsync();
    }
}