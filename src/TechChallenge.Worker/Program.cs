using TechChallenge.Infrastructure;
using TechChallenge.Worker.Services;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddInfrastructure();
        services.AddHostedService<KafkaConsumerService>();
    })
    .Build();

await host.RunAsync();