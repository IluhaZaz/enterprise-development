using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CarRental.Infrastructure.EfCore.Migrations;

/// <inheritdoc />
public partial class Initial : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "car_models",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                SeatsNumber = table.Column<int>(type: "integer", nullable: false),
                DriveType = table.Column<int>(type: "integer", nullable: true),
                BodyType = table.Column<int>(type: "integer", nullable: true),
                Class = table.Column<int>(type: "integer", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_car_models", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "clients",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                LastName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                FirstName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                Patronymic = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                BirthDate = table.Column<DateOnly>(type: "date", nullable: false),
                DriverLicense = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_clients", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "model_generations",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Year = table.Column<int>(type: "integer", nullable: true),
                EngineVolume = table.Column<double>(type: "double precision", nullable: true),
                ModelId = table.Column<int>(type: "integer", nullable: false),
                TransmissionType = table.Column<int>(type: "integer", nullable: true),
                PricePerHour = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_model_generations", x => x.Id);
                table.ForeignKey(
                    name: "FK_model_generations_car_models_ModelId",
                    column: x => x.ModelId,
                    principalTable: "car_models",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "cars",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                GenerationId = table.Column<int>(type: "integer", nullable: false),
                LicensePlate = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                Color = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_cars", x => x.Id);
                table.ForeignKey(
                    name: "FK_cars_model_generations_GenerationId",
                    column: x => x.GenerationId,
                    principalTable: "model_generations",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "rental_logs",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                CarId = table.Column<int>(type: "integer", nullable: false),
                ClientId = table.Column<int>(type: "integer", nullable: false),
                RentStartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                Duration = table.Column<double>(type: "double precision", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_rental_logs", x => x.Id);
                table.ForeignKey(
                    name: "FK_rental_logs_cars_CarId",
                    column: x => x.CarId,
                    principalTable: "cars",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_rental_logs_clients_ClientId",
                    column: x => x.ClientId,
                    principalTable: "clients",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.InsertData(
            table: "car_models",
            columns: new[] { "Id", "BodyType", "Class", "DriveType", "Name", "SeatsNumber" },
            values: new object[,]
            {
                { 1, 2, 3, 0, "Toyota Camry", 5 },
                { 2, 3, 4, 2, "BMW X5", 5 },
                { 3, 2, 1, 0, "Lada Vesta", 5 },
                { 4, 0, 1, 0, "Kia Rio", 5 },
                { 5, 2, 5, 1, "Mercedes S-Class", 5 },
                { 6, 0, 1, 0, "Volkswagen Polo", 5 },
                { 7, 4, 2, 0, "Hyundai Creta", 5 },
                { 8, 1, 2, 0, "Skoda Octavia", 5 }
            });

        migrationBuilder.InsertData(
            table: "clients",
            columns: new[] { "Id", "BirthDate", "DriverLicense", "FirstName", "LastName", "Patronymic" },
            values: new object[,]
            {
                { 1, new DateOnly(1985, 5, 15), "7712345678", "Ivan", "Ivanov", "Ivanovich" },
                { 2, new DateOnly(1990, 8, 22), "7723456789", "Petr", "Petrov", "Petrovich" },
                { 3, new DateOnly(1988, 3, 10), "7734567890", "Alexey", "Sidorov", "Sergeevich" },
                { 4, new DateOnly(1992, 11, 5), "7745678901", "Dmitry", "Smirnov", "Andreevich" },
                { 5, new DateOnly(1987, 7, 18), "7756789012", "Sergey", "Kuznetsov", "Mikhailovich" },
                { 6, new DateOnly(1995, 2, 28), "7767890123", "Mikhail", "Popov", "Dmitrievich" },
                { 7, new DateOnly(1983, 9, 12), "7778901234", "Andrey", "Volkov", "Alexeevich" },
                { 8, new DateOnly(1991, 6, 8), "7789012345", "Pavel", "Sokolov", "Nikolaevich" },
                { 9, new DateOnly(1989, 4, 25), "7790123456", "Nikolay", "Novikov", "Vladimirovich" },
                { 10, new DateOnly(1993, 12, 3), "7701234567", "Vladimir", "Morozov", "Olegovich" }
            });

        migrationBuilder.InsertData(
            table: "model_generations",
            columns: new[] { "Id", "EngineVolume", "ModelId", "PricePerHour", "TransmissionType", "Year" },
            values: new object[,]
            {
                { 1, 2.5, 1, 2000m, 0, 2020 },
                { 2, 2.0, 1, 2200m, 0, 2022 },
                { 3, 3.0, 2, 3500m, 0, 2021 },
                { 4, 1.6000000238418579, 3, 900m, 1, 2019 },
                { 5, 1.6000000238418579, 4, 1100m, 2, 2020 },
                { 6, 3.0, 5, 5000m, 0, 2022 },
                { 7, 1.3999999761581421, 6, 950m, 1, 2021 },
                { 8, 2.0, 7, 1800m, 0, 2023 },
                { 9, 1.7999999523162842, 8, 1700m, 0, 2022 },
                { 10, 1.3999999761581421, 6, 1000m, 3, 2020 }
            });

        migrationBuilder.InsertData(
            table: "cars",
            columns: new[] { "Id", "Color", "GenerationId", "LicensePlate" },
            values: new object[,]
            {
                { 1, "Black", 1, "A123BC777" },
                { 2, "White", 1, "B456OP777" },
                { 3, "Silver", 2, "E789TT777" },
                { 4, "Blue", 3, "K321MM777" },
                { 5, "Black", 3, "M654HH777" },
                { 6, "Red", 4, "O987PP777" },
                { 7, "White", 4, "P159CC777" },
                { 8, "Gray", 5, "C753YY777" },
                { 9, "Black", 6, "T456FF777" },
                { 10, "White", 6, "Y789XX777" },
                { 11, "Blue", 7, "X123KK777" },
                { 12, "Green", 8, "C456LL777" },
                { 13, "Silver", 9, "Z789MM777" },
                { 14, "Red", 10, "H321HH777" }
            });

        migrationBuilder.InsertData(
            table: "rental_logs",
            columns: new[] { "Id", "CarId", "ClientId", "Duration", "RentStartDate" },
            values: new object[,]
            {
                { 1, 1, 1, 24.0, new DateTime(2024, 1, 10, 10, 0, 0, 0, DateTimeKind.Unspecified) },
                { 2, 1, 2, 48.0, new DateTime(2024, 1, 15, 14, 0, 0, 0, DateTimeKind.Unspecified) },
                { 3, 1, 3, 12.0, new DateTime(2024, 2, 1, 9, 0, 0, 0, DateTimeKind.Unspecified) },
                { 4, 2, 4, 72.0, new DateTime(2024, 1, 20, 16, 0, 0, 0, DateTimeKind.Unspecified) },
                { 5, 2, 1, 24.0, new DateTime(2024, 2, 5, 11, 0, 0, 0, DateTimeKind.Unspecified) },
                { 6, 3, 5, 36.0, new DateTime(2024, 1, 25, 8, 0, 0, 0, DateTimeKind.Unspecified) },
                { 7, 4, 6, 24.0, new DateTime(2024, 2, 10, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                { 8, 4, 2, 48.0, new DateTime(2024, 2, 12, 10, 0, 0, 0, DateTimeKind.Unspecified) },
                { 9, 5, 7, 12.0, new DateTime(2024, 1, 30, 15, 0, 0, 0, DateTimeKind.Unspecified) },
                { 10, 6, 8, 60.0, new DateTime(2024, 2, 3, 14, 0, 0, 0, DateTimeKind.Unspecified) },
                { 11, 7, 1, 18.0, new DateTime(2024, 2, 7, 16, 0, 0, 0, DateTimeKind.Unspecified) },
                { 12, 8, 4, 24.0, new DateTime(2024, 2, 9, 11, 0, 0, 0, DateTimeKind.Unspecified) },
                { 13, 9, 5, 36.0, new DateTime(2024, 2, 11, 10, 0, 0, 0, DateTimeKind.Unspecified) },
                { 14, 10, 6, 48.0, new DateTime(2024, 2, 13, 12, 0, 0, 0, DateTimeKind.Unspecified) },
                { 15, 11, 7, 24.0, new DateTime(2024, 2, 14, 8, 0, 0, 0, DateTimeKind.Unspecified) },
                { 16, 14, 8, 12.0, new DateTime(2024, 2, 15, 14, 0, 0, 0, DateTimeKind.Unspecified) },
                { 17, 12, 9, 48.0, new DateTime(2024, 2, 16, 9, 0, 0, 0, DateTimeKind.Unspecified) },
                { 18, 13, 10, 24.0, new DateTime(2024, 2, 17, 16, 0, 0, 0, DateTimeKind.Unspecified) },
                { 19, 1, 3, 36.0, new DateTime(2024, 2, 18, 11, 0, 0, 0, DateTimeKind.Unspecified) },
                { 20, 2, 5, 24.0, new DateTime(2024, 2, 19, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                { 21, 4, 7, 18.0, new DateTime(2024, 2, 20, 10, 0, 0, 0, DateTimeKind.Unspecified) },
                { 22, 6, 9, 72.0, new DateTime(2024, 2, 21, 15, 0, 0, 0, DateTimeKind.Unspecified) },
                { 23, 9, 1, 24.0, new DateTime(2024, 2, 22, 12, 0, 0, 0, DateTimeKind.Unspecified) },
                { 24, 11, 2, 36.0, new DateTime(2024, 2, 23, 9, 0, 0, 0, DateTimeKind.Unspecified) }
            });

        migrationBuilder.CreateIndex(
            name: "IX_car_models_Name",
            table: "car_models",
            column: "Name");

        migrationBuilder.CreateIndex(
            name: "IX_cars_GenerationId",
            table: "cars",
            column: "GenerationId");

        migrationBuilder.CreateIndex(
            name: "IX_cars_LicensePlate",
            table: "cars",
            column: "LicensePlate");

        migrationBuilder.CreateIndex(
            name: "IX_model_generations_ModelId",
            table: "model_generations",
            column: "ModelId");

        migrationBuilder.CreateIndex(
            name: "IX_rental_logs_CarId",
            table: "rental_logs",
            column: "CarId");

        migrationBuilder.CreateIndex(
            name: "IX_rental_logs_ClientId",
            table: "rental_logs",
            column: "ClientId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "rental_logs");

        migrationBuilder.DropTable(
            name: "cars");

        migrationBuilder.DropTable(
            name: "clients");

        migrationBuilder.DropTable(
            name: "model_generations");

        migrationBuilder.DropTable(
            name: "car_models");
    }
}
