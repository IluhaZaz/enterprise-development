var builder = DistributedApplication.CreateBuilder(args);

var db = builder.AddPostgres("car-rental").AddDatabase("car-rental-db");

var messaging = builder.AddRabbitMQ("rabbitMqGenerator")
    .WithManagementPlugin();

var queueName = "rental-log-create";

builder.AddProject<Projects.CarRental_Api>("car-rental-api")
    .WithReference(db, "Database")
    .WithReference(messaging)
    .WithEnvironment("RabbitMq__QueueName", queueName)
    .WaitFor(db)
    .WaitFor(messaging);

builder.AddProject<Projects.CarRental_Generator>("car-rental-generator")
    .WithReference(messaging)
    .WithEnvironment("Generator__IntervalMs", "5000")
    .WithEnvironment("Generator__BatchSize", "2")
    .WithEnvironment("Generator__MaxCarId", "24")
    .WithEnvironment("Generator__MaxClientId", "20")
    .WithEnvironment("Generator__QueueName", queueName)
    .WaitFor(messaging);

builder.Build().Run();