using AutoMapper;
using CarRental.Application;
using CarRental.Application.Contracts;
using CarRental.Application.Interfaces;
using CarRental.Application.Services;
using CarRental.Domain.Entities;
using CarRental.Domain.Interfaces;
using CarRental.Domain.TestData;
using CarRental.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

var mapperConfig = new MapperConfiguration(
    config => config.AddProfile(new MapProfile()),
    LoggerFactory.Create(builder => builder.AddConsole()));
IMapper? mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

CarRentalDataSeed data = new CarRentalDataSeed();

builder.Services.AddSingleton<IRepository<CarModel, int>, CarModelRepository>(_ => new CarModelRepository(data.CarModels));
builder.Services.AddSingleton<IRepository<ModelGeneration, int>, ModelGenerationRepository>(_ => new ModelGenerationRepository(data.ModelGenerations));
builder.Services.AddSingleton<IRepository<Car, int>, CarRepository>(_ => new CarRepository(data.Cars));
builder.Services.AddSingleton<IRepository<Client, int>, ClientRepository>(_ => new ClientRepository(data.Clients));
builder.Services.AddSingleton<IRepository<RentalLog, int>, RentalLogRepository>(_ => new RentalLogRepository(data.RentalLogs));

builder.Services.AddScoped<IService<CarModelCreate, CarModelGet>, CarModelService>();
builder.Services.AddScoped<ModelGenerationService>();
builder.Services.AddScoped<CarService>();
builder.Services.AddScoped<IService<ClientCreate, ClientGet>, ClientService>();
builder.Services.AddScoped<RentalLogService>();

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
