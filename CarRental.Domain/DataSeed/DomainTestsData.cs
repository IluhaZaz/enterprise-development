using CarRental.Domain.Entities;
using CarRental.Domain.Enums;
using System.Drawing;

namespace CarRental.Domain.TestData;

/// <summary>
/// Class containing data for unit tests
/// </summary>
public class CarRentalDataSeed
{
    /// <summary>
    /// Contains different car models for testing
    /// </summary>
    public List<CarModel> CarModels { get; }

    /// <summary>
    /// Contains car models generatinos for testing
    /// </summary>
    public List<ModelGeneration> ModelGenerations { get; }

    /// <summary>
    /// Contains different cars for testing
    /// </summary>
    public List<Car> Cars { get; }

    /// <summary>
    /// Contains different clients for testing
    /// </summary>
    public List<Client> Clients { get; }

    /// <summary>
    /// Contains different rent contract logs for testing
    /// </summary>
    public List<RentalLog> RentalLogs { get; }

    public CarRentalDataSeed()
    { 
        CarModels = new List<CarModel>
        {
            new CarModel { Id = 1, Name = "Toyota Camry", SeatsNumber = 5, DriveType = CarDriveType.FrontWheel, BodyType = CarBodyType.Saloon, Class = CarClass.D },
            new CarModel { Id = 2, Name = "BMW X5", SeatsNumber = 5, DriveType = CarDriveType.AllWheel, BodyType = CarBodyType.SportsUtilityVehicle, Class = CarClass.E },
            new CarModel { Id = 3, Name = "Lada Vesta", SeatsNumber = 5, DriveType = CarDriveType.FrontWheel, BodyType = CarBodyType.Saloon, Class = CarClass.B },
            new CarModel { Id = 4, Name = "Kia Rio", SeatsNumber = 5, DriveType = CarDriveType.FrontWheel, BodyType = CarBodyType.Hatchback, Class = CarClass.B },
            new CarModel { Id = 5, Name = "Mercedes S-Class", SeatsNumber = 5, DriveType = CarDriveType.RearWheel, BodyType = CarBodyType.Saloon, Class = CarClass.F },
            new CarModel { Id = 6, Name = "Volkswagen Polo", SeatsNumber = 5, DriveType = CarDriveType.FrontWheel, BodyType = CarBodyType.Hatchback, Class = CarClass.B },
            new CarModel { Id = 7, Name = "Hyundai Creta", SeatsNumber = 5, DriveType = CarDriveType.FrontWheel, BodyType = CarBodyType.CrossoverUtilityVehicle, Class = CarClass.C },
            new CarModel { Id = 8, Name = "Skoda Octavia", SeatsNumber = 5, DriveType = CarDriveType.FrontWheel, BodyType = CarBodyType.Estate, Class = CarClass.C }
        };

        ModelGenerations = new List<ModelGeneration>
        {
            new ModelGeneration { Id = 1, Year = 2020, EngineVolume = 2.5f, Model = CarModels[0], TransmissionType = CarTransmissionType.Automatic, PricePerHour = 2000 },
            new ModelGeneration { Id = 2, Year = 2022, EngineVolume = 2.0f, Model = CarModels[0], TransmissionType = CarTransmissionType.Automatic, PricePerHour = 2200 },
            new ModelGeneration { Id = 3, Year = 2021, EngineVolume = 3.0f, Model = CarModels[1], TransmissionType = CarTransmissionType.Automatic, PricePerHour = 3500 },
            new ModelGeneration { Id = 4, Year = 2019, EngineVolume = 1.6f, Model = CarModels[2], TransmissionType = CarTransmissionType.Manual, PricePerHour = 900 },
            new ModelGeneration { Id = 5, Year = 2020, EngineVolume = 1.6f, Model = CarModels[3], TransmissionType = CarTransmissionType.AutomatedManual, PricePerHour = 1100 },
            new ModelGeneration { Id = 6, Year = 2022, EngineVolume = 3.0f, Model = CarModels[4], TransmissionType = CarTransmissionType.Automatic, PricePerHour = 5000 },
            new ModelGeneration { Id = 7, Year = 2021, EngineVolume = 1.4f, Model = CarModels[5], TransmissionType = CarTransmissionType.Manual, PricePerHour = 950 },
            new ModelGeneration { Id = 8, Year = 2023, EngineVolume = 2.0f, Model = CarModels[6], TransmissionType = CarTransmissionType.Automatic, PricePerHour = 1800 },
            new ModelGeneration { Id = 9, Year = 2022, EngineVolume = 1.8f, Model = CarModels[7], TransmissionType = CarTransmissionType.Automatic, PricePerHour = 1700 },
            new ModelGeneration { Id = 10, Year = 2020, EngineVolume = 1.4f, Model = CarModels[5], TransmissionType = CarTransmissionType.ContinuouslyVariable, PricePerHour = 1000 }
        };

        Cars = new List<Car>
        {
            new Car { Id = 1, Generation = ModelGenerations[0], LicensePlate = "A123BC777", Color = Color.Black },
            new Car { Id = 2, Generation = ModelGenerations[0], LicensePlate = "B456OP777", Color = Color.White },
            new Car { Id = 3, Generation = ModelGenerations[1], LicensePlate = "E789TT777", Color = Color.Silver },
            new Car { Id = 4, Generation = ModelGenerations[2], LicensePlate = "K321MM777", Color = Color.Blue },
            new Car { Id = 5, Generation = ModelGenerations[2], LicensePlate = "M654HH777", Color = Color.Black },
            new Car { Id = 6, Generation = ModelGenerations[3], LicensePlate = "O987PP777", Color = Color.Red },
            new Car { Id = 7, Generation = ModelGenerations[3], LicensePlate = "P159CC777", Color = Color.White },
            new Car { Id = 8, Generation = ModelGenerations[4], LicensePlate = "C753YY777", Color = Color.Gray },
            new Car { Id = 9, Generation = ModelGenerations[5], LicensePlate = "T456FF777", Color = Color.Black },
            new Car { Id = 10, Generation = ModelGenerations[5], LicensePlate = "Y789XX777", Color = Color.White },
            new Car { Id = 11, Generation = ModelGenerations[6], LicensePlate = "X123KK777", Color = Color.Blue },
            new Car { Id = 12, Generation = ModelGenerations[7], LicensePlate = "C456LL777", Color = Color.Green },
            new Car { Id = 13, Generation = ModelGenerations[8], LicensePlate = "×789MM777", Color = Color.Silver },
            new Car { Id = 14, Generation = ModelGenerations[9], LicensePlate = "H321HH777", Color = Color.Red }
        };

        Clients = new List<Client>
        {
            new Client { Id = 1, LastName = "Ivanov", FirstName = "Ivan", Patronymic = "Ivanovich", BirthDate = new DateOnly(1985, 5, 15), DriverLicense = "7712345678" },
            new Client { Id = 2, LastName = "Petrov", FirstName = "Petr", Patronymic = "Petrovich", BirthDate = new DateOnly(1990, 8, 22), DriverLicense = "7723456789" },
            new Client { Id = 3, LastName = "Sidorov", FirstName = "Alexey", Patronymic = "Sergeevich", BirthDate = new DateOnly(1988, 3, 10), DriverLicense = "7734567890" },
            new Client { Id = 4, LastName = "Smirnov", FirstName = "Dmitry", Patronymic = "Andreevich", BirthDate = new DateOnly(1992, 11, 5), DriverLicense = "7745678901" },
            new Client { Id = 5, LastName = "Kuznetsov", FirstName = "Sergey", Patronymic = "Mikhailovich", BirthDate = new DateOnly(1987, 7, 18), DriverLicense = "7756789012" },
            new Client { Id = 6, LastName = "Popov", FirstName = "Mikhail", Patronymic = "Dmitrievich", BirthDate = new DateOnly(1995, 2, 28), DriverLicense = "7767890123" },
            new Client { Id = 7, LastName = "Volkov", FirstName = "Andrey", Patronymic = "Alexeevich", BirthDate = new DateOnly(1983, 9, 12), DriverLicense = "7778901234" },
            new Client { Id = 8, LastName = "Sokolov", FirstName = "Pavel", Patronymic = "Nikolaevich", BirthDate = new DateOnly(1991, 6, 8), DriverLicense = "7789012345" },
            new Client { Id = 9, LastName = "Novikov", FirstName = "Nikolay", Patronymic = "Vladimirovich", BirthDate = new DateOnly(1989, 4, 25), DriverLicense = "7790123456" },
            new Client { Id = 10, LastName = "Morozov", FirstName = "Vladimir", Patronymic = "Olegovich", BirthDate = new DateOnly(1993, 12, 3), DriverLicense = "7701234567" }
        };

        RentalLogs = new List<RentalLog>
        {
            // Toyota Camry (most popular)
            new RentalLog { Id = 1, Car = Cars[0], Client = Clients[0], RentStartDate = new DateTime(2024, 1, 10, 10, 0, 0), Duration = 24 },
            new RentalLog { Id = 2, Car = Cars[0], Client = Clients[1], RentStartDate = new DateTime(2024, 1, 15, 14, 0, 0), Duration = 48 },
            new RentalLog { Id = 3, Car = Cars[0], Client = Clients[2], RentStartDate = new DateTime(2024, 2, 1, 9, 0, 0), Duration = 12 },
            new RentalLog { Id = 4, Car = Cars[1], Client = Clients[3], RentStartDate = new DateTime(2024, 1, 20, 16, 0, 0), Duration = 72 },
            new RentalLog { Id = 5, Car = Cars[1], Client = Clients[0], RentStartDate = new DateTime(2024, 2, 5, 11, 0, 0), Duration = 24 },
            new RentalLog { Id = 6, Car = Cars[2], Client = Clients[4], RentStartDate = new DateTime(2024, 1, 25, 8, 0, 0), Duration = 36 },
        
            // BMW X5
            new RentalLog { Id = 7, Car = Cars[3], Client = Clients[5], RentStartDate = new DateTime(2024, 2, 10, 13, 0, 0), Duration = 24 },
            new RentalLog { Id = 8, Car = Cars[3], Client = Clients[1], RentStartDate = new DateTime(2024, 2, 12, 10, 0, 0), Duration = 48 },
            new RentalLog { Id = 9, Car = Cars[4], Client = Clients[6], RentStartDate = new DateTime(2024, 1, 30, 15, 0, 0), Duration = 12 },
        
            // Lada Vesta
            new RentalLog { Id = 10, Car = Cars[5], Client = Clients[7], RentStartDate = new DateTime(2024, 2, 3, 14, 0, 0), Duration = 60 },
            new RentalLog { Id = 11, Car = Cars[6], Client = Clients[0], RentStartDate = new DateTime(2024, 2, 7, 16, 0, 0), Duration = 18 },
        
            // Kia Rio
            new RentalLog { Id = 12, Car = Cars[7], Client = Clients[3], RentStartDate = new DateTime(2024, 2, 9, 11, 0, 0), Duration = 24 },
        
            // Mercedes S-Class
            new RentalLog { Id = 13, Car = Cars[8], Client = Clients[4], RentStartDate = new DateTime(2024, 2, 11, 10, 0, 0), Duration = 36 },
            new RentalLog { Id = 14, Car = Cars[9], Client = Clients[5], RentStartDate = new DateTime(2024, 2, 13, 12, 0, 0), Duration = 48 },
        
            // Volkswagen Polo
            new RentalLog { Id = 15, Car = Cars[10], Client = Clients[6], RentStartDate = new DateTime(2024, 2, 14, 8, 0, 0), Duration = 24 },
            new RentalLog { Id = 16, Car = Cars[13], Client = Clients[7], RentStartDate = new DateTime(2024, 2, 15, 14, 0, 0), Duration = 12 },
        
            // Hyundai Creta
            new RentalLog { Id = 17, Car = Cars[11], Client = Clients[8], RentStartDate = new DateTime(2024, 2, 16, 9, 0, 0), Duration = 48 },
        
            // Skoda Octavia
            new RentalLog { Id = 18, Car = Cars[12], Client = Clients[9], RentStartDate = new DateTime(2024, 2, 17, 16, 0, 0), Duration = 24 },
        
            // Other rents for statistics
            new RentalLog { Id = 19, Car = Cars[0], Client = Clients[2], RentStartDate = new DateTime(2024, 2, 18, 11, 0, 0), Duration = 36 },
            new RentalLog { Id = 20, Car = Cars[1], Client = Clients[4], RentStartDate = new DateTime(2024, 2, 19, 13, 0, 0), Duration = 24 },
            new RentalLog { Id = 21, Car = Cars[3], Client = Clients[6], RentStartDate = new DateTime(2024, 2, 20, 10, 0, 0), Duration = 18 },
            new RentalLog { Id = 22, Car = Cars[5], Client = Clients[8], RentStartDate = new DateTime(2024, 2, 21, 15, 0, 0), Duration = 72 },
            new RentalLog { Id = 23, Car = Cars[8], Client = Clients[0], RentStartDate = new DateTime(2024, 2, 22, 12, 0, 0), Duration = 24 },
            new RentalLog { Id = 24, Car = Cars[10], Client = Clients[1], RentStartDate = new DateTime(2024, 2, 23, 9, 0, 0), Duration = 36 }
        };
    }
}