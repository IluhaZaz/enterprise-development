using System.Drawing;
using CarRental.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CarRental.Infrastructure.EfCore;

/// <summary>
/// Entity Framework Core DbContext for CarRental application
/// Configures entity mappings, constraints, relations and type conversions
/// </summary>
public class CarRentalDbContext(DbContextOptions options) : DbContext(options)
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

            e.Property(x => x.Id).ValueGeneratedOnAdd();

            e.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            e.Property(x => x.SeatsNumber)
                .IsRequired();

            e.Property(x => x.DriveType);
            e.Property(x => x.BodyType);
            e.Property(x => x.Class);

            e.HasIndex(x => x.Name);
        });

        modelBuilder.Entity<ModelGeneration>(e =>
        {
            e.ToTable("model_generations");
            e.HasKey(x => x.Id);

            e.Property(x => x.Id).ValueGeneratedOnAdd();

            e.Property(x => x.Year);
            e.Property(x => x.EngineVolume);

            e.Property(x => x.TransmissionType);

            e.Property(x => x.PricePerHour)
                .IsRequired()
                .HasPrecision(12, 2);

            e.Property(x => x.ModelId).IsRequired();

            e.HasOne(x => x.Model)
                .WithMany()
                .HasForeignKey(x => x.ModelId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            e.HasIndex(x => x.ModelId);
        });

        modelBuilder.Entity<Car>(e =>
        {
            e.ToTable("cars");
            e.HasKey(x => x.Id);

            e.Property(x => x.Id).ValueGeneratedOnAdd();

            e.Property(x => x.LicensePlate)
                .IsRequired()
                .HasMaxLength(16);

            e.Property(x => x.Color)
                .HasConversion(colorToString)
                .HasMaxLength(16);

            e.Property(x => x.GenerationId).IsRequired();

            e.HasOne(x => x.Generation)
                .WithMany()
                .HasForeignKey(x => x.GenerationId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            e.HasIndex(x => x.LicensePlate);
            e.HasIndex(x => x.GenerationId);
        });

        modelBuilder.Entity<Client>(e =>
        {
            e.ToTable("clients");
            e.HasKey(x => x.Id);

            e.Property(x => x.Id).ValueGeneratedOnAdd();

            e.Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(100);

            e.Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            e.Property(x => x.Patronymic)
                .IsRequired()
                .HasMaxLength(100);

            e.Property(x => x.BirthDate)
                .IsRequired();

            e.Property(x => x.DriverLicense)
                .IsRequired()
                .HasMaxLength(32);
        });

        modelBuilder.Entity<RentalLog>(e =>
        {
            e.ToTable("rental_logs");
            e.HasKey(x => x.Id);

            e.Property(x => x.Id).ValueGeneratedOnAdd();

            e.Property(x => x.RentStartDate)
                .IsRequired();

            e.Property(x => x.Duration)
                .IsRequired();

            e.Property(x => x.CarId).IsRequired();
            e.Property(x => x.ClientId).IsRequired();

            e.HasOne(x => x.Car)
                .WithMany()
                .HasForeignKey(x => x.CarId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            e.HasOne(x => x.Client)
                .WithMany()
                .HasForeignKey(x => x.ClientId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            e.HasIndex(x => x.CarId);
            e.HasIndex(x => x.ClientId);
        });
    }
}