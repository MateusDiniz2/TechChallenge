using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TechChallenge.Domain.Interfaces;
using TechChallenge.Infrastructure.Context;
using TechChallenge.Infrastructure.Repositories;

namespace TechChallenge.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Configura MongoDbContext
        services.AddSingleton<MongoDbContext>();
        services.AddSingleton(sp =>
        {
            var context = sp.GetRequiredService<MongoDbContext>();
            return context.Database;
        });

        // Repositórios
        services.AddScoped<IProductRepository, ProductRepository>();

        return services;
    }
}
