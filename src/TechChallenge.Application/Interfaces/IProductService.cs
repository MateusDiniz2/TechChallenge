using TechChallenge.Domain.Entities;

namespace TechChallenge.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(string id);
        Task<Product> CreateAsync(Product product);
        Task<bool> UpdateAsync(string id, Product product);
        Task<bool> DeleteAsync(string id);
    }
}