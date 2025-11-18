using AutoMapper;
using CarRental.Application.Contracts;
using CarRental.Domain.Entities;

namespace CarRental.Application;
public class MapProfile : Profile
{
    public MapProfile()
    {
        CreateMap<CarModelGet, CarModel>().ReverseMap();
        CreateMap<CarModelCreate, CarModel>().ReverseMap();

        CreateMap<ModelGenerationGet, ModelGeneration>().ReverseMap();
        CreateMap<ModelGenerationCreate, ModelGeneration>().ReverseMap();

        CreateMap<CarGet, Car>().ReverseMap();
        CreateMap<CarCreate, Car>().ReverseMap();

        CreateMap<ClientGet, Client>().ReverseMap();
        CreateMap<ClientCreate, Client>().ReverseMap();

        CreateMap<RentalLogGet, RentalLog>().ReverseMap();
        CreateMap<RentalLogCreate, RentalLog>().ReverseMap();
    }
}
