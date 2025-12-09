using CarRental.Domain.Entities;
using CarRental.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Infrastructure.EfCore.Repositories;

/// <summary>
/// EF Core repository for Client entity with CRUD operations
/// </summary>
public class ClientEfCoreRepository(CarRentalDbContext context) : IRepository<Client, int>
{
    /// <summary>
    /// Create new client entity and return its id
    /// </summary>
    public async Task<int> Create(Client entity)
    {
        await context.Clients.AddAsync(entity);
        await context.SaveChangesAsync();
        return entity.Id;
    }

    /// <summary>
    /// Delete client entity by id and return operation result
    /// </summary>
    public async Task<bool> Delete(int entityId)
    {
        var entity = await context.Clients.FirstOrDefaultAsync(e => e.Id == entityId);
        if (entity == null)
            return false;

        context.Clients.Remove(entity);
        await context.SaveChangesAsync();

        return true;
    }

    /// <summary>
    /// Read client entity by id
    /// </summary>
    public async Task<Client?> Read(int entityId) =>
        await context.Clients.FirstOrDefaultAsync(e => e.Id == entityId);

    /// <summary>
    /// Read all client entities
    /// </summary>
    public async Task<List<Client>> ReadAll() =>
        await context.Clients.ToListAsync();

    /// <summary>
    /// Update client entity data
    /// </summary>
    public async Task Update(Client entity)
    {
        context.Clients.Update(entity);

        await context.SaveChangesAsync();
    }
}