using Microsoft.Extensions.DependencyInjection;
using TechChallenge.Application.UseCases;

namespace TechChallenge.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<ProductService>();
            // Adicione outros serviços aqui quando precisar

            return services;
        }
    }
}
