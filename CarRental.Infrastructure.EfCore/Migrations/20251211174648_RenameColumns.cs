using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRental.Infrastructure.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class RenameColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cars_model_generations_GenerationId",
                table: "cars");

            migrationBuilder.DropForeignKey(
                name: "FK_model_generations_car_models_ModelId",
                table: "model_generations");

            migrationBuilder.DropForeignKey(
                name: "FK_rental_logs_cars_CarId",
                table: "rental_logs");

            migrationBuilder.DropForeignKey(
                name: "FK_rental_logs_clients_ClientId",
                table: "rental_logs");

            migrationBuilder.RenameColumn(
                name: "Duration",
                table: "rental_logs",
                newName: "duration");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "rental_logs",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "RentStartDate",
                table: "rental_logs",
                newName: "rent_start_date");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                table: "rental_logs",
                newName: "client_id");

            migrationBuilder.RenameColumn(
                name: "CarId",
                table: "rental_logs",
                newName: "car_id");

            migrationBuilder.RenameIndex(
                name: "IX_rental_logs_ClientId",
                table: "rental_logs",
                newName: "ix_rental_logs_client_id");

            migrationBuilder.RenameIndex(
                name: "IX_rental_logs_CarId",
                table: "rental_logs",
                newName: "ix_rental_logs_car_id");

            migrationBuilder.RenameColumn(
                name: "Year",
                table: "model_generations",
                newName: "year");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "model_generations",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "TransmissionType",
                table: "model_generations",
                newName: "transmission_type");

            migrationBuilder.RenameColumn(
                name: "PricePerHour",
                table: "model_generations",
                newName: "price_per_hour");

            migrationBuilder.RenameColumn(
                name: "ModelId",
                table: "model_generations",
                newName: "model_id");

            migrationBuilder.RenameColumn(
                name: "EngineVolume",
                table: "model_generations",
                newName: "engine_volume");

            migrationBuilder.RenameIndex(
                name: "IX_model_generations_ModelId",
                table: "model_generations",
                newName: "ix_model_generations_model_id");

            migrationBuilder.RenameColumn(
                name: "Patronymic",
                table: "clients",
                newName: "patronymic");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "clients",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "clients",
                newName: "last_name");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "clients",
                newName: "first_name");

            migrationBuilder.RenameColumn(
                name: "DriverLicense",
                table: "clients",
                newName: "driver_license");

            migrationBuilder.RenameColumn(
                name: "BirthDate",
                table: "clients",
                newName: "birth_date");

            migrationBuilder.RenameColumn(
                name: "Color",
                table: "cars",
                newName: "color");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "cars",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "LicensePlate",
                table: "cars",
                newName: "license_plate");

            migrationBuilder.RenameColumn(
                name: "GenerationId",
                table: "cars",
                newName: "generation_id");

            migrationBuilder.RenameIndex(
                name: "IX_cars_LicensePlate",
                table: "cars",
                newName: "ix_cars_license_plate");

            migrationBuilder.RenameIndex(
                name: "IX_cars_GenerationId",
                table: "cars",
                newName: "ix_cars_generation_id");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "car_models",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Class",
                table: "car_models",
                newName: "class");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "car_models",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "SeatsNumber",
                table: "car_models",
                newName: "seats_number");

            migrationBuilder.RenameColumn(
                name: "DriveType",
                table: "car_models",
                newName: "drive_type");

            migrationBuilder.RenameColumn(
                name: "BodyType",
                table: "car_models",
                newName: "body_type");

            migrationBuilder.RenameIndex(
                name: "IX_car_models_Name",
                table: "car_models",
                newName: "ix_car_models_name");

            migrationBuilder.AddForeignKey(
                name: "FK_cars_model_generations_generation_id",
                table: "cars",
                column: "generation_id",
                principalTable: "model_generations",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_model_generations_car_models_model_id",
                table: "model_generations",
                column: "model_id",
                principalTable: "car_models",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_rental_logs_cars_car_id",
                table: "rental_logs",
                column: "car_id",
                principalTable: "cars",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_rental_logs_clients_client_id",
                table: "rental_logs",
                column: "client_id",
                principalTable: "clients",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cars_model_generations_generation_id",
                table: "cars");

            migrationBuilder.DropForeignKey(
                name: "FK_model_generations_car_models_model_id",
                table: "model_generations");

            migrationBuilder.DropForeignKey(
                name: "FK_rental_logs_cars_car_id",
                table: "rental_logs");

            migrationBuilder.DropForeignKey(
                name: "FK_rental_logs_clients_client_id",
                table: "rental_logs");

            migrationBuilder.RenameColumn(
                name: "duration",
                table: "rental_logs",
                newName: "Duration");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "rental_logs",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "rent_start_date",
                table: "rental_logs",
                newName: "RentStartDate");

            migrationBuilder.RenameColumn(
                name: "client_id",
                table: "rental_logs",
                newName: "ClientId");

            migrationBuilder.RenameColumn(
                name: "car_id",
                table: "rental_logs",
                newName: "CarId");

            migrationBuilder.RenameIndex(
                name: "ix_rental_logs_client_id",
                table: "rental_logs",
                newName: "IX_rental_logs_ClientId");

            migrationBuilder.RenameIndex(
                name: "ix_rental_logs_car_id",
                table: "rental_logs",
                newName: "IX_rental_logs_CarId");

            migrationBuilder.RenameColumn(
                name: "year",
                table: "model_generations",
                newName: "Year");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "model_generations",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "transmission_type",
                table: "model_generations",
                newName: "TransmissionType");

            migrationBuilder.RenameColumn(
                name: "price_per_hour",
                table: "model_generations",
                newName: "PricePerHour");

            migrationBuilder.RenameColumn(
                name: "model_id",
                table: "model_generations",
                newName: "ModelId");

            migrationBuilder.RenameColumn(
                name: "engine_volume",
                table: "model_generations",
                newName: "EngineVolume");

            migrationBuilder.RenameIndex(
                name: "ix_model_generations_model_id",
                table: "model_generations",
                newName: "IX_model_generations_ModelId");

            migrationBuilder.RenameColumn(
                name: "patronymic",
                table: "clients",
                newName: "Patronymic");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "clients",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "last_name",
                table: "clients",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "first_name",
                table: "clients",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "driver_license",
                table: "clients",
                newName: "DriverLicense");

            migrationBuilder.RenameColumn(
                name: "birth_date",
                table: "clients",
                newName: "BirthDate");

            migrationBuilder.RenameColumn(
                name: "color",
                table: "cars",
                newName: "Color");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "cars",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "license_plate",
                table: "cars",
                newName: "LicensePlate");

            migrationBuilder.RenameColumn(
                name: "generation_id",
                table: "cars",
                newName: "GenerationId");

            migrationBuilder.RenameIndex(
                name: "ix_cars_license_plate",
                table: "cars",
                newName: "IX_cars_LicensePlate");

            migrationBuilder.RenameIndex(
                name: "ix_cars_generation_id",
                table: "cars",
                newName: "IX_cars_GenerationId");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "car_models",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "class",
                table: "car_models",
                newName: "Class");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "car_models",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "seats_number",
                table: "car_models",
                newName: "SeatsNumber");

            migrationBuilder.RenameColumn(
                name: "drive_type",
                table: "car_models",
                newName: "DriveType");

            migrationBuilder.RenameColumn(
                name: "body_type",
                table: "car_models",
                newName: "BodyType");

            migrationBuilder.RenameIndex(
                name: "ix_car_models_name",
                table: "car_models",
                newName: "IX_car_models_Name");

            migrationBuilder.AddForeignKey(
                name: "FK_cars_model_generations_GenerationId",
                table: "cars",
                column: "GenerationId",
                principalTable: "model_generations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_model_generations_car_models_ModelId",
                table: "model_generations",
                column: "ModelId",
                principalTable: "car_models",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_rental_logs_cars_CarId",
                table: "rental_logs",
                column: "CarId",
                principalTable: "cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_rental_logs_clients_ClientId",
                table: "rental_logs",
                column: "ClientId",
                principalTable: "clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
