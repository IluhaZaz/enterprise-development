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
    public TKey Create(TEntity entity);

    /// <summary>
    /// Update entity's data
    /// </summary>
    public void Update(TEntity entity);

    /// <summary>
    /// Delete entity by ID
    /// </summary>
    public bool Delete(TKey id);

    /// <summary>
    /// Return all entities from storage
    /// </summary>
    public List<TEntity> ReadAll();

    /// <summary>
    /// Return entity from storage by id
    /// </summary>
    public TEntity? Read(TKey id);
}