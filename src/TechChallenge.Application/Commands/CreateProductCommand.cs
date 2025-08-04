using TechChallenge.Application.Interfaces;
using TechChallenge.Domain.Entities;

namespace TechChallenge.Application.Commands
{
    public class CreateProductCommand : IRequest<Product>
    {
        public string Name { get; }
        public string Sku { get; }
        public string Description { get; }
        public decimal Price { get; }
        public int StockQuantity { get; }

        public CreateProductCommand(string name, string sku, string description, decimal price, int stockQuantity)
        {
            Name = name;
            Sku = sku;
            Description = description;
            Price = price;
            StockQuantity = stockQuantity;
        }
    }
}
