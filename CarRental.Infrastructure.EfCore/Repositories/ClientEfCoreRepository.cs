using CarRental.Domain.Entities;
using CarRental.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Infrastructure.EfCore.Repositories;

public class ClientEfCoreRepository(CarRentalDbContext context) : IRepository<Client, int>
{
    public async Task<int> Create(Client entity)
    {
        await context.Clients.AddAsync(entity);
        await context.SaveChangesAsync();
        return entity.Id;
    }

    public async Task<bool> Delete(int entityId)
    {
        var entity = await context.Clients.FirstOrDefaultAsync(e => e.Id == entityId);
        if (entity == null)
            return false;

        context.Clients.Remove(entity);
        await context.SaveChangesAsync();

        return true;
    }

    public async Task<Client?> Read(int entityId) =>
        await context.Clients.FirstOrDefaultAsync(e => e.Id == entityId);

    public async Task<List<Client>> ReadAll() =>
        await context.Clients.ToListAsync();

    public async Task Update(Client entity)
    {
        context.Clients.Update(entity);
        await context.SaveChangesAsync();
    }
}