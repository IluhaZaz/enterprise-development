namespace CarRental.Application.Interfaces;

/// <summary>
/// Provide CRUD operation for entity
/// </summary>
public interface IService<TEntityCreateDTO, TEntityGetDTO>
{
    // <summary>
    /// Create new entity instance and return it's ID
    /// </summary>
    public Task<int> Create(TEntityCreateDTO entity_dto);

    /// <summary>
    /// Update entity's data
    /// </summary>
    public Task Update(TEntityCreateDTO entity_dto, int entity_id);

    /// <summary>
    /// Delete entity by ID
    /// </summary>
    public Task<bool> Delete(int id);

    /// <summary>
    /// Return all entities from storage
    /// </summary>
    public Task<List<TEntityGetDTO>> ReadAll();

    /// <summary>
    /// Return entity from storage by id
    /// </summary>
    public Task<TEntityGetDTO?> Read(int id);
}