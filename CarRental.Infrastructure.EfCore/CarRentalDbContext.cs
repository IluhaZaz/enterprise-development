using System.Drawing;
using CarRental.Domain.DataSeed;
using CarRental.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CarRental.Infrastructure.EfCore;

/// <summary>
/// Entity Framework Core DbContext for CarRental application
/// Configures entity mappings, constraints, relations and type conversions
/// </summary>
public class CarRentalDbContext(DbContextOptions options, CarRentalDataSeed dataSeed) : DbContext(options)
{
    /// <summary>
    /// Cars table set
    /// </summary>
    public DbSet<Car> Cars => Set<Car>();

    /// <summary>
    /// Car models table set
    /// </summary>
    public DbSet<CarModel> CarModels => Set<CarModel>();

    /// <summary>
    /// Model generations table set
    /// </summary>
    public DbSet<ModelGeneration> ModelGenerations => Set<ModelGeneration>();

    /// <summary>
    /// Clients table set
    /// </summary>
    public DbSet<Client> Clients => Set<Client>();

    /// <summary>
    /// Rental logs table set
    /// </summary>
    public DbSet<RentalLog> RentalLogs => Set<RentalLog>();

    /// <summary>
    /// Configure entity schema: table names, columns, relations, indexes and conversions
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var colorToString = new ValueConverter<Color?, string?>(
            c => c.HasValue ? ColorTranslator.ToHtml(c.Value) : null,
            s => string.IsNullOrWhiteSpace(s) ? null : ColorTranslator.FromHtml(s)
        );

        modelBuilder.Entity<CarModel>(e =>
        {
            e.ToTable("car_models");

            e.HasKey(x => x.Id);

            e.Property(x => x.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            e.Property(x => x.Name)
                .HasColumnName("name")
                .IsRequired()
                .HasMaxLength(100);

            e.Property(x => x.SeatsNumber)
                .HasColumnName("seats_number")
                .IsRequired();

            e.Property(x => x.DriveType)
                .HasColumnName("drive_type");

            e.Property(x => x.BodyType)
                .HasColumnName("body_type");

            e.Property(x => x.Class)
                .HasColumnName("class");

            e.HasIndex(x => x.Name)
                .HasDatabaseName("ix_car_models_name");

            e.HasData(dataSeed.CarModels);
        });

        modelBuilder.Entity<ModelGeneration>(e =>
        {
            e.ToTable("model_generations");

            e.HasKey(x => x.Id);

            e.Property(x => x.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            e.Property(x => x.Year)
                .HasColumnName("year");

            e.Property(x => x.EngineVolume)
                .HasColumnName("engine_volume");

            e.Property(x => x.TransmissionType)
                .HasColumnName("transmission_type");

            e.Property(x => x.PricePerHour)
                .HasColumnName("price_per_hour")
                .IsRequired()
                .HasPrecision(12, 2);

            e.Property(x => x.ModelId)
                .HasColumnName("model_id")
                .IsRequired();

            e.HasOne(x => x.Model)
                .WithMany()
                .HasForeignKey(x => x.ModelId)
                .OnDelete(DeleteBehavior.Restrict);

            e.HasIndex(x => x.ModelId)
                .HasDatabaseName("ix_model_generations_model_id");

            e.HasData(dataSeed.ModelGenerations);
        });

        modelBuilder.Entity<Car>(e =>
        {
            e.ToTable("cars");

            e.HasKey(x => x.Id);

            e.Property(x => x.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            e.Property(x => x.LicensePlate)
                .HasColumnName("license_plate")
                .IsRequired()
                .HasMaxLength(16);

            e.Property(x => x.Color)
                .HasColumnName("color")
                .HasConversion(colorToString)
                .HasMaxLength(16);

            e.Property(x => x.GenerationId)
                .HasColumnName("generation_id")
                .IsRequired();

            e.HasOne(x => x.Generation)
                .WithMany()
                .HasForeignKey(x => x.GenerationId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            e.HasIndex(x => x.LicensePlate)
                .HasDatabaseName("ix_cars_license_plate");

            e.HasIndex(x => x.GenerationId)
                .HasDatabaseName("ix_cars_generation_id");

            e.HasData(dataSeed.Cars);
        });

        modelBuilder.Entity<Client>(e =>
        {
            e.ToTable("clients");

            e.HasKey(x => x.Id);

            e.Property(x => x.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            e.Property(x => x.LastName)
                .HasColumnName("last_name")
                .IsRequired()
                .HasMaxLength(100);

            e.Property(x => x.FirstName)
                .HasColumnName("first_name")
                .IsRequired()
                .HasMaxLength(100);

            e.Property(x => x.Patronymic)
                .HasColumnName("patronymic")
                .IsRequired()
                .HasMaxLength(100);

            e.Property(x => x.BirthDate)
                .HasColumnName("birth_date")
                .IsRequired();

            e.Property(x => x.DriverLicense)
                .HasColumnName("driver_license")
                .IsRequired()
                .HasMaxLength(32);

            e.HasData(dataSeed.Clients);
        });

        modelBuilder.Entity<RentalLog>(e =>
        {
            e.ToTable("rental_logs");

            e.HasKey(x => x.Id);

            e.Property(x => x.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            e.Property(x => x.RentStartDate)
                .HasColumnName("rent_start_date")
                .IsRequired();

            e.Property(x => x.Duration)
                .HasColumnName("duration")
                .IsRequired();

            e.Property(x => x.CarId)
                .HasColumnName("car_id")
                .IsRequired();

            e.Property(x => x.ClientId)
                .HasColumnName("client_id")
                .IsRequired();

            e.HasOne(x => x.Car)
                .WithMany()
                .HasForeignKey(x => x.CarId)
                .OnDelete(DeleteBehavior.Restrict);

            e.HasOne(x => x.Client)
                .WithMany()
                .HasForeignKey(x => x.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            e.HasIndex(x => x.CarId)
                .HasDatabaseName("ix_rental_logs_car_id");

            e.HasIndex(x => x.ClientId)
                .HasDatabaseName("ix_rental_logs_client_id");

            e.HasData(dataSeed.RentalLogs);
        });
    }
}