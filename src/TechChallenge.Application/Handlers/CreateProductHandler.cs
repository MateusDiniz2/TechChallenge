using TechChallenge.Application.Commands;
using TechChallenge.Application.Interfaces;
using TechChallenge.Domain.Entities;
using TechChallenge.Domain.Interfaces;

namespace TechChallenge.Application.Handlers
{
    public class CreateProductHandler : IHandler<CreateProductCommand, Product>
    {
        private readonly IProductRepository _repository;

        public CreateProductHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<Product> HandleAsync(CreateProductCommand command)
        {
            var product = new Product(command.Name, command.Sku, command.Description, command.Price, command.StockQuantity);
            await _repository.CreateAsync(product);
            return product;
        }
    }
}
