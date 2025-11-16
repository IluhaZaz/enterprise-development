namespace CarRental.Application.Interfaces;

public interface IService<TEntityCreateDTO, TEntityGetDTO>
{
    public int Create(TEntityCreateDTO entity_dto);

    public void Update(TEntityCreateDTO entity_dto);

    public bool Delete(int id);

    public List<TEntityGetDTO> ReadAll();

    public TEntityGetDTO? Read(int id);
}