using TechChallenge.Application.Interfaces;

namespace TechChallenge.Application.Commands
{
    public class UpdateProductCommand : IRequest<bool>
    {
        public Guid Id { get; }
        public string Name { get; }
        public string Description { get; }
        public decimal Price { get; }
        public int StockQuantity { get; }

        public UpdateProductCommand(Guid id, string name, string description, decimal price, int stockQuantity)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            StockQuantity = stockQuantity;
        }
    }
}
