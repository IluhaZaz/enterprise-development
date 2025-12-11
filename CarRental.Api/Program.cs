using AutoMapper;
using CarRental.Application;
using CarRental.Application.Contracts;
using CarRental.Application.Interfaces;
using CarRental.Application.Services;
using CarRental.Domain.DataSeed;
using CarRental.Domain.Entities;
using CarRental.Domain.Interfaces;
using CarRental.Infrastructure.EfCore;
using CarRental.Infrastructure.EfCore.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

var mapperConfig = new MapperConfiguration(
    config => config.AddProfile(new MapProfile()),
    LoggerFactory.Create(builder => builder.AddConsole()));
IMapper? mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddControllers().AddJsonOptions(o =>
{
    o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSingleton<CarRentalDataSeed>();

builder.Services.AddTransient<IRepository<CarModel, int>, CarModelEfCoreRepository>();
builder.Services.AddTransient<IRepository<ModelGeneration, int>, ModelGenerationEfCoreRepository>();
builder.Services.AddTransient<IRepository<Car, int>, CarEfCoreRepository>();
builder.Services.AddTransient<IRepository<Client, int>, ClientEfCoreRepository>();
builder.Services.AddTransient<IRepository<RentalLog, int>, RentalLogEfCoreRepository>();

builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();
builder.Services.AddScoped<IService<CarModelCreate, CarModelGet>, CarModelService>();
builder.Services.AddScoped<ModelGenerationService>();
builder.Services.AddScoped<CarService>();
builder.Services.AddScoped<ClientService>();
builder.Services.AddScoped<RentalLogService>();

builder.Services.AddSwaggerGen(c =>
{
    var assemblies = AppDomain.CurrentDomain.GetAssemblies()
        .Where(a => a.GetName().Name!.StartsWith("CarRental"))
        .Distinct();

    foreach (var assembly in assemblies)
    {
        var xmlFile = $"{assembly.GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        if (File.Exists(xmlPath))
            c.IncludeXmlComments(xmlPath);
    }
});

builder.AddNpgsqlDbContext<CarRentalDbContext>("Database");

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<CarRentalDbContext>();

    await context.Database.MigrateAsync();
}

app.MapDefaultEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
