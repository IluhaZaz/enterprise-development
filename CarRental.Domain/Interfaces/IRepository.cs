namespace CarRental.Domain.Interfaces;

/// <summary>
/// In memory repository for entities
/// </summary>
public interface IRepository<TEntity, TKey>
    where TEntity : class
{
    /// <summary>
    /// Create new entity instance and return it's ID
    /// </summary>
    public Task<TKey> Create(TEntity entity);

    /// <summary>
    /// Update entity's data
    /// </summary>
    public Task Update(TEntity entity);

    /// <summary>
    /// Delete entity by ID
    /// </summary>
    public Task<bool> Delete(TKey id);

    /// <summary>
    /// Return all entities from storage
    /// </summary>
    public Task<List<TEntity>> ReadAll();

    /// <summary>
    /// Return entity from storage by id
    /// </summary>
    public Task<TEntity?> Read(TKey id);
}