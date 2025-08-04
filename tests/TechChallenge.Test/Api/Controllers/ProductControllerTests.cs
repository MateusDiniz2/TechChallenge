using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using TechChallenge.API.Controllers;
using TechChallenge.Application.Commands;
using TechChallenge.Application.Interfaces;
using TechChallenge.Application.Queries;
using TechChallenge.Domain.Entities;

namespace TechChallenge.Tests.Api.Controllers
{
    public class ProductControllerTests
    {
        private readonly IDispatcher _dispatcherMock;
        private readonly ProductController _controller;

        public ProductControllerTests()
        {
            _dispatcherMock = Substitute.For<IDispatcher>();
            _controller = new ProductController(_dispatcherMock);
        }

        [Fact]
        public async Task CreateAsync_ValidCommand_ReturnsCreatedAtAction()
        {
            // Arrange
            var command = new CreateProductCommand("Produto Teste", "SKU001", "Descrição", 10, 5);
            var expectedProduct = new Product(command.Name, command.Sku, command.Description, command.Price, command.StockQuantity);

            _dispatcherMock.SendAsync(command).Returns(expectedProduct);

            // Act
            var result = await _controller.CreateAsync(command);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(nameof(ProductController.GetByIdAsync), createdResult.ActionName);
            Assert.Equal(expectedProduct.Id, createdResult.RouteValues["id"]);
            Assert.Equal(expectedProduct, createdResult.Value);

            await _dispatcherMock.Received(1).SendAsync(command);
        }

        [Fact]
        public async Task GetByIdAsync_ProductExists_ReturnsOk()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var expectedProduct = new Product("Nome", "SKU", "Desc", 10, 5);
            var query = new GetProductByIdQuery(productId);

            _dispatcherMock.SendAsync(query).Returns(expectedProduct);

            // Act
            var result = await _controller.GetByIdAsync(productId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(expectedProduct, okResult.Value);
        }

        [Fact]
        public async Task GetByIdAsync_ProductNotFound_ReturnsNotFound()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var query = new GetProductByIdQuery(productId);

            _dispatcherMock.SendAsync(query).Returns((Product?)null);

            // Act
            var result = await _controller.GetByIdAsync(productId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task UpdateAsync_IdMismatch_ReturnsBadRequest()
        {
            // Arrange
            var command = new UpdateProductCommand(Guid.NewGuid(), "Nome", "SKU", 10, 100);
            var routeId = Guid.NewGuid(); // diferente do command.Id

            // Act
            var result = await _controller.UpdateAsync(routeId, command);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Id da rota diferente do id do comando.", badRequest.Value);
        }

        [Fact]
        public async Task UpdateAsync_ValidCommand_ReturnsNoContent_WhenUpdated()
        {
            // Arrange
            var id = Guid.NewGuid();
            var command = new UpdateProductCommand(id, "Nome", "SKU", 10, 5);

            _dispatcherMock.SendAsync(command).Returns(true);

            // Act
            var result = await _controller.UpdateAsync(id, command);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdateAsync_NotFound_ReturnsNotFound()
        {
            // Arrange
            var id = Guid.NewGuid();
            var command = new UpdateProductCommand(id, "Nome", "SKU", 10, 5);

            _dispatcherMock.SendAsync(command).Returns(false);

            // Act
            var result = await _controller.UpdateAsync(id, command);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteAsync_ExistingProduct_ReturnsNoContent()
        {
            // Arrange
            var id = Guid.NewGuid();
            var command = new DeleteProductCommand(id);

            _dispatcherMock.SendAsync(command).Returns(true);

            // Act
            var result = await _controller.DeleteAsync(id);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteAsync_NotFound_ReturnsNotFound()
        {
            // Arrange
            var id = Guid.NewGuid();
            var command = new DeleteProductCommand(id);

            _dispatcherMock.SendAsync(command).Returns(false);

            // Act
            var result = await _controller.DeleteAsync(id);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsOkWithProducts()
        {
            // Arrange
            var query = new GetAllProductsQuery();
            var products = new List<Product>
            {
                new Product("Produto1", "SKU1", "Desc1", 10, 5),
                new Product("Produto2", "SKU2", "Desc2", 20, 3)
            };

            _dispatcherMock.SendAsync(query).Returns(products);

            // Act
            var result = await _controller.GetAllAsync();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(products, okResult.Value);
        }
    }
}
