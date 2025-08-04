using TechChallenge.Domain.Entities;

namespace TechChallenge.Domain.Interfaces;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(string id);
    Task<IEnumerable<Product>> GetAllAsync();
    Task CreateAsync(Product product);
    Task UpdateAsync(Product product);
    Task DeleteAsync(string id);
}
