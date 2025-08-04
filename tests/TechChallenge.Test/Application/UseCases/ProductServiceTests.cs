using NSubstitute;
using TechChallenge.Application.UseCases;
using TechChallenge.Domain.Entities;
using TechChallenge.Domain.Interfaces;

namespace TechChallenge.Tests.Application.UseCases
{
    public class ProductServiceTests
    {
        private readonly IProductRepository _repositoryMock;
        private readonly ProductService _service;

        public ProductServiceTests()
        {
            _repositoryMock = Substitute.For<IProductRepository>();
            _service = new ProductService(_repositoryMock);
        }

        [Fact]
        public async Task GetAllAsync_Returns_All_Products()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product("Produto1", "SKU1", "Desc1", 10, 5),
                new Product("Produto2", "SKU2", "Desc2", 20, 10)
            };
            _repositoryMock.GetAllAsync().Returns(products);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.Equal(products, result);
            await _repositoryMock.Received(1).GetAllAsync();
        }

        [Fact]
        public async Task GetByIdAsync_Returns_Product_When_Found()
        {
            // Arrange
            var product = new Product("Produto1", "SKU1", "Desc1", 10, 5);
            _repositoryMock.GetByIdAsync(product.Id).Returns(product);

            // Act
            var result = await _service.GetByIdAsync(product.Id);

            // Assert
            Assert.Equal(product, result);
            await _repositoryMock.Received(1).GetByIdAsync(product.Id);
        }

        [Fact]
        public async Task GetByIdAsync_Returns_Null_When_Not_Found()
        {
            // Arrange
            var id = Guid.NewGuid();
            _repositoryMock.GetByIdAsync(id).Returns((Product?)null);

            // Act
            var result = await _service.GetByIdAsync(id);

            // Assert
            Assert.Null(result);
            await _repositoryMock.Received(1).GetByIdAsync(id);
        }

        [Fact]
        public async Task CreateAsync_Calls_Repository_And_Returns_Product()
        {
            // Arrange
            var product = new Product("Produto Novo", "SKU123", "Desc", 30, 15);

            // Act
            var result = await _service.CreateAsync(product);

            // Assert
            await _repositoryMock.Received(1).CreateAsync(product);
            Assert.Equal(product, result);
        }

        [Fact]
        public async Task UpdateAsync_Returns_True_When_Product_Exists_And_Updates()
        {
            // Arrange
            var id = Guid.NewGuid();
            var existingProduct = new Product("Produto Existente", "SKUEX", "Desc", 50, 20);
            var updatedProduct = new Product("Produto Atualizado", "SKUEX", "Desc Atualizada", 60, 25);

            _repositoryMock.GetByIdAsync(id).Returns(existingProduct);

            // Act
            var result = await _service.UpdateAsync(id, updatedProduct);

            // Assert
            Assert.True(result);
            await _repositoryMock.Received(1).GetByIdAsync(id);
            await _repositoryMock.Received(1).UpdateAsync(existingProduct);
            
            // Verifica se propriedades foram atualizadas
            Assert.Equal(updatedProduct.Name, existingProduct.Name);
            Assert.Equal(updatedProduct.Description, existingProduct.Description);
            Assert.Equal(updatedProduct.Price, existingProduct.Price);
            Assert.Equal(updatedProduct.StockQuantity, existingProduct.StockQuantity);
        }

        [Fact]
        public async Task UpdateAsync_Returns_False_When_Product_Not_Found()
        {
            // Arrange
            var id = Guid.NewGuid();
            var updatedProduct = new Product("Produto Atualizado", "SKUEX", "Desc Atualizada", 60, 25);

            _repositoryMock.GetByIdAsync(id).Returns((Product?)null);

            // Act
            var result = await _service.UpdateAsync(id, updatedProduct);

            // Assert
            Assert.False(result);
            await _repositoryMock.Received(1).GetByIdAsync(id);
            await _repositoryMock.DidNotReceive().UpdateAsync(Arg.Any<Product>());
        }

        [Fact]
        public async Task DeleteAsync_Returns_True_When_Product_Exists_And_Deletes()
        {
            // Arrange
            var id = Guid.NewGuid();
            var existingProduct = new Product("Produto Existente", "SKUEX", "Desc", 50, 20);
            _repositoryMock.GetByIdAsync(id).Returns(existingProduct);

            // Act
            var result = await _service.DeleteAsync(id);

            // Assert
            Assert.True(result);
            await _repositoryMock.Received(1).GetByIdAsync(id);
            await _repositoryMock.Received(1).DeleteAsync(id);
        }

        [Fact]
        public async Task DeleteAsync_Returns_False_When_Product_Not_Found()
        {
            // Arrange
            var id = Guid.NewGuid();
            _repositoryMock.GetByIdAsync(id).Returns((Product?)null);

            // Act
            var result = await _service.DeleteAsync(id);

            // Assert
            Assert.False(result);
            await _repositoryMock.Received(1).GetByIdAsync(id);
            await _repositoryMock.DidNotReceive().DeleteAsync(id);
        }
    }
}
