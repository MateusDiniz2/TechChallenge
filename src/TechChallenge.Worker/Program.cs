using TechChallenge.Application.Interfaces;
using TechChallenge.Application.UseCases;
using TechChallenge.Infrastructure;
using TechChallenge.Worker.Services;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddInfrastructure();
        services.AddScoped<IProductService, ProductService>();
        services.AddHostedService<KafkaConsumerService>();
    })
    .Build();

await host.RunAsync();