using CarRental.Generator;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();
builder.AddRabbitMQClient("rabbitMqGenerator");

builder.Services.AddOptions<GeneratorOptions>()
    .Bind(builder.Configuration.GetSection(GeneratorOptions.SectionName));

builder.Services.AddSingleton<RentalLogGenerator>();
builder.Services.AddSingleton<RabbitMqPublisher>();
builder.Services.AddHostedService<GeneratorWorker>();

var host = builder.Build();
host.Run();