var builder = DistributedApplication.CreateBuilder(args);

var db = builder.AddPostgres("car-rental").AddDatabase("car-rental-db");

builder.AddProject<Projects.CarRental_Api>("car-rental-api")
    .WithReference(db, "Database")
    .WaitFor(db);

builder.Build().Run();