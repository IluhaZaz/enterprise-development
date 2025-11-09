namespace CarRental.Domain.Interfaces;


public interface IRepository<TEntity, TKey>
    where TEntity : class
{
    public TKey Create(TEntity entity);

    public void Update(TEntity entity);

    public bool Delete(TKey id);

    public List<TEntity> ReadAll();

    public TEntity? Read(TKey id);
}