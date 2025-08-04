using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace TechChallenge.Domain.Entities;

public class Product
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Required(ErrorMessage = "O nome do produto é obrigatório.")]
    [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
    public string Name { get; private set; }

    [Required(ErrorMessage = "O SKU do produto é obrigatório.")]
    [StringLength(50, ErrorMessage = "O SKU deve ter no máximo 50 caracteres.")]
    public string Sku { get; private set; }

    [StringLength(500, ErrorMessage = "A descrição deve ter no máximo 500 caracteres.")]
    public string Description { get; private set; }

    [Range(0, double.MaxValue, ErrorMessage = "O preço deve ser maior ou igual a zero.")]
    public decimal Price { get; private set; }

    [Range(0, int.MaxValue, ErrorMessage = "A quantidade em estoque deve ser maior ou igual a zero.")]
    public int StockQuantity { get; private set; }

    public Product(string name, string sku, string description, decimal price, int stockQuantity)
    {
        Id = Guid.NewGuid().ToString();
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
