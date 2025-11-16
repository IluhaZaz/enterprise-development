using AutoMapper;
using CarRental.Application;
using CarRental.Application.Contracts;
using CarRental.Application.Interfaces;
using CarRental.Application.Services;
using CarRental.Domain.Entities;
using CarRental.Domain.Interfaces;
using CarRental.Infrastructure.Repositories.InMemory;

var builder = WebApplication.CreateBuilder(args);

var mapperConfig = new MapperConfiguration(
    config => config.AddProfile(new MapProfile()),
    LoggerFactory.Create(builder => builder.AddConsole()));
IMapper? mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSingleton<IRepository<CarModel, int>, CarModelRepository>();
builder.Services.AddSingleton<IRepository<ModelGeneration, int>, ModelGenerationRepository>();
builder.Services.AddSingleton<IRepository<Car, int>, CarRepository>();
builder.Services.AddSingleton<IRepository<Client, int>, ClientRepository>();
builder.Services.AddSingleton<IRepository<RentalLog, int>, RentalLogRepository>();

builder.Services.AddScoped<IService<CarModelCreate, CarModelGet>, CarModelService>();
builder.Services.AddScoped<IService<ModelGenerationCreate, ModelGenerationGet>, ModelGenerationService>();
builder.Services.AddScoped<IService<CarCreate, CarGet>, CarService>();
builder.Services.AddScoped<IService<ClientCreate, ClientGet>, ClientService>();
builder.Services.AddScoped<IService<RentalLogCreate, RentalLogGet>, RentalLogService>();

builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
