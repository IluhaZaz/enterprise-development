using AutoMapper;
using CarRental.Application.Contracts;
using CarRental.Application.Interfaces;
using CarRental.Domain.Entities;
using CarRental.Domain.Interfaces;

namespace CarRental.Application.Services;
public class ModelGenerationService(
    IRepository<ModelGeneration, int> repository,
    IRepository<CarModel, int> modelRepository, 
    IMapper mapper) : IService<ModelGenerationCreate, ModelGenerationGet>
{
    public int Create(ModelGenerationCreate entity_dto)
    {
        CarModel model = modelRepository.Read(entity_dto.ModelId)
             ?? throw new KeyNotFoundException($"CarModel(id={entity_dto.ModelId}) does not exist");

        ModelGeneration entity = mapper.Map<ModelGeneration>(entity_dto);
        entity.Model = model;

        repository.Create(entity);
        return entity.Id;
    }

    public void Update(ModelGenerationCreate entity_dto)
    {
        CarModel model = modelRepository.Read(entity_dto.ModelId)
             ?? throw new KeyNotFoundException($"CarModel(id={entity_dto.ModelId}) does not exist");

        ModelGeneration entity = mapper.Map<ModelGeneration>(entity_dto);
        entity.Model = model;

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
