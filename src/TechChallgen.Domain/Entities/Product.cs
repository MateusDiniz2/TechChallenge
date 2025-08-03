namespace TechChallenge.Domain.Entities;

public class Product
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Sku { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; }
    public int StockQuantity { get; private set; }

    public Product(string name, string sku, string description, decimal price, int stockQuantity)
    {
        Id = Guid.NewGuid();
        Name = name;
        Sku = sku;
        Description = description;
        Price = price;
        StockQuantity = stockQuantity;
    }

    public void UpdateStock(int quantity)
    {
        StockQuantity = quantity;
    }

    public void UpdateDetails(string name, string description, decimal price)
    {
        Name = name;
        Description = description;
        Price = price;
    }
}
