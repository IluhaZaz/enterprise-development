var builder = DistributedApplication.CreateBuilder(args);

var db = builder.AddPostgres("car-rental").AddDatabase("car-rental-db");

var messaging = builder.AddRabbitMQ("rabbitMqGenerator")
    .WithManagementPlugin();

var queueName = builder.Configuration["RabbitMq:QueueName"];
var generatorIntervalMs = builder.Configuration["Generator:IntervalMs"];
var generatorBatchSize = builder.Configuration["Generator:BatchSize"];
var generatorMaxCarId = builder.Configuration["Generator:MaxCarId"];
var generatorMaxClientId = builder.Configuration["Generator:MaxClientId"];

var api = builder.AddProject<Projects.CarRental_Api>("car-rental-api")
    .WithReference(db, "Database")
    .WithReference(messaging)
    .WithEnvironment("RabbitMq__QueueName", queueName)
    .WaitFor(db)
    .WaitFor(messaging);

builder.AddProject<Projects.CarRental_Generator>("car-rental-generator")
    .WithReference(messaging)
    .WithEnvironment("Generator__IntervalMs", generatorIntervalMs)
    .WithEnvironment("Generator__BatchSize", generatorBatchSize)
    .WithEnvironment("Generator__MaxCarId", generatorMaxCarId)
    .WithEnvironment("Generator__MaxClientId", generatorMaxClientId)
    .WithEnvironment("Generator__QueueName", queueName)
    .WaitFor(messaging)
    .WaitFor(api);

builder.Build().Run();