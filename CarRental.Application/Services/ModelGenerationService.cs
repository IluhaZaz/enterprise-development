using CarRental.Application.Interfaces;
using CarRental.Infrastructure.Repositories.InMemory;
using CarRental.Application.Contracts;
using AutoMapper;
using CarRental.Domain.Entities;

namespace CarRental.Application.Services;
public class ModelGenerationService(ModelGenerationRepository repository, IMapper mapper) : IService<ModelGenerationCreate, ModelGenerationGet>
{
    public int Create(ModelGenerationCreate entity_dto)
    {
        ModelGeneration entity = mapper.Map<ModelGeneration>(entity_dto);
        repository.Create(entity);
        return entity.Id;
    }

    public void Update(ModelGenerationCreate entity_dto)
    {
        ModelGeneration entity = mapper.Map<ModelGeneration>(entity_dto);
        repository.Update(entity);
    }

    public bool Delete(int id)
    {
        return repository.Delete(id);
    }

    public List<ModelGenerationGet> ReadAll()
    {
        List<ModelGeneration> res = repository.ReadAll();
        return mapper.Map<List<ModelGenerationGet>>(res);
    }

    public ModelGenerationGet? Read(int id)
    {
        ModelGeneration? entity = repository.Read(id);
        return mapper.Map<ModelGenerationGet>(entity);
    }
}
