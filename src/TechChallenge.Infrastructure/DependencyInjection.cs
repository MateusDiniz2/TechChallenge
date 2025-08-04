using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TechChallenge.Domain.Interfaces;
using TechChallenge.Infrastructure.Context;
using TechChallenge.Infrastructure.Messaging;
using TechChallenge.Infrastructure.Repositories;
using static TechChallenge.Application.Interfaces.IKafkaClient;

namespace TechChallenge.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        // Configura MongoDbContext
        services.AddSingleton<MongoDbContext>();
        services.AddSingleton(sp =>
        {
            var context = sp.GetRequiredService<MongoDbContext>();
            return context.Database;
        });

        services.AddDbContext<ProductDbContext>(options =>
                options.UseInMemoryDatabase("TechChallengeDb"));

        services.AddSingleton<IKafkaProducer>(sp =>
                new KafkaProducer("localhost:9092", "products-topic"));

        services.AddSingleton<IKafkaConsumer>(sp =>
            new KafkaConsumer("localhost:9092", "products-topic", "product-consumer-group"));

        // Repositórios
        services.AddScoped<IProductRepository, ProductRepository>();

        return services;
    }
}
