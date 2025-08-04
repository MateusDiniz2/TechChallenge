using Microsoft.Extensions.DependencyInjection;
using TechChallenge.Application.Commands;
using TechChallenge.Application.Dispatcher;
using TechChallenge.Application.Handlers;
using TechChallenge.Application.Interfaces;
using TechChallenge.Application.Queries;
using TechChallenge.Application.UseCases;
using TechChallenge.Domain.Entities;

namespace TechChallenge.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<ProductService>();
            services.AddScoped<IDispatcher, Dispatcher>();

            // Handlers
            services.AddScoped<IHandler<CreateProductCommand, Product>, CreateProductHandler>();
            services.AddScoped<IHandler<UpdateProductCommand, bool>, UpdateProductHandler>();
            services.AddScoped<IHandler<DeleteProductCommand, bool>, DeleteProductHandler>();
            services.AddScoped<IHandler<GetAllProductsQuery, IEnumerable<Product>>, GetAllProductsHandler>();
            services.AddScoped<IHandler<GetProductByIdQuery, Product?>, GetProductByIdHandler>();

            return services;
        }
    }
}
