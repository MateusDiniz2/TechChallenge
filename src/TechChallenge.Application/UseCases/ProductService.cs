
using TechChallenge.Domain.Interfaces;
using TechChallenge.Domain.Entities;

namespace TechChallenge.Application.UseCases;

public class ProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Product?> GetByIdAsync(Guid id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task AddAsync(Product product)
    {
        await _repository.AddAsync(product);
    }

    public async Task UpdateAsync(Product product)
    {
        await _repository.UpdateAsync(product);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _repository.DeleteAsync(id);
    }
}
