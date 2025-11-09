using CarRental.Domain.Entities;
using CarRental.Domain.Interfaces;

namespace CarRental.Infrastructure.Repositories.InMemory;

public class ModelGenerationRepository : IRepository<ModelGeneration, int>
{
    private readonly List<ModelGeneration> _modelGenerations;
    private int _currId;

    public ModelGenerationRepository()
    {
        _modelGenerations = new List<ModelGeneration>();
        _currId = 1;
    }

    public int Create(ModelGeneration entity)
    {
        entity.Id = _currId;
        _modelGenerations.Add(entity);
        _currId++;

        return entity.Id;
    }

    public void Update(ModelGeneration entity)
    {
        Delete(entity.Id);
        _modelGenerations.Add(entity);
    }

    public bool Delete(int id)
    {
        ModelGeneration? modelGeneration = Read(id);
        if (modelGeneration != null)
        {
            _modelGenerations.Remove(modelGeneration);
            return true;
        }
        return false;
    }

    public List<ModelGeneration> ReadAll()
    {
        return [.. _modelGenerations];
    }

    public ModelGeneration? Read(int id)
    {
        return _modelGenerations.FirstOrDefault(c => c.Id == id);
    }
}
