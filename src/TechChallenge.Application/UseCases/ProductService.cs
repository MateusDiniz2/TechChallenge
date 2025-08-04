using TechChallenge.Application.Interfaces;
using TechChallenge.Domain.Entities;
using TechChallenge.Domain.Interfaces;

namespace TechChallenge.Application.UseCases
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _productRepository.GetAllAsync();
        }

        public async Task<Product?> GetByIdAsync(string id)
        {
            return await _productRepository.GetByIdAsync(id);
        }

        public async Task<Product> CreateAsync(Product product)
        {
            await _productRepository.CreateAsync(product);
            return product;
        }

        public async Task<bool> UpdateAsync(string id, Product updatedProduct)
        {
            var existingProduct = await _productRepository.GetByIdAsync(id);
            if (existingProduct == null)
                return false;

            existingProduct.UpdateDetails(
                updatedProduct.Name,
                updatedProduct.Description,
                updatedProduct.Price);

            existingProduct.UpdateStock(updatedProduct.StockQuantity);

            await _productRepository.UpdateAsync(existingProduct);

            return true;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var existing = await _productRepository.GetByIdAsync(id);
            if (existing == null)
                return false;

            await _productRepository.DeleteAsync(id);
            return true;
        }
    }
}
