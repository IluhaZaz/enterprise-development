using CarRental.Generator;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();
builder.AddRabbitMQClient("rabbitMqGenerator");

builder.Services.AddHostedService<GeneratorWorker>();

var host = builder.Build();
host.Run();