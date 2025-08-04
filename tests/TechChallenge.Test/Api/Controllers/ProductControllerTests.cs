using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using TechChallenge.API.Controllers;
using TechChallenge.Application.Interfaces;
using TechChallenge.Domain.Entities;

namespace TechChallenge.Tests.Api.Controllers
{
    public class ProductControllerTests
    {
        private readonly IProductService _serviceMock;
        private readonly ProductController _controller;

        public ProductControllerTests()
        {
            _serviceMock = Substitute.For<IProductService>();
            _controller = new ProductController(_serviceMock);
        }

        [Fact]
        public async Task CreateAsync_InvalidModel_ReturnsBadRequest_And_DoesNotCallService()
        {
            // Arrange
            _controller.ModelState.AddModelError("Name", "O nome é obrigatório."); // Simula erro de validação
            var product = new Product("", "SKU001", "Desc", 10, 5);

            // Act
            var result = await _controller.CreateAsync(product);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
            await _serviceMock.DidNotReceiveWithAnyArgs().CreateAsync(default);
        }

        [Fact]
        public async Task CreateAsync_ValidModel_CallsService_And_ReturnsCreatedAtAction()
        {
            // Arrange
            var product = new Product("Produto Teste", "SKU001", "Descrição", 10, 5);

            // Configura retorno do serviço
            _serviceMock.CreateAsync(product).Returns(Task.FromResult(product));

            // Act
            var result = await _controller.CreateAsync(product);

            // Assert tipo da resposta
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);

            // Verifica rota de localização no header
            Assert.Equal(nameof(ProductController.GetByIdAsync), createdResult.ActionName);

            // Verifica parâmetro id da rota
            Assert.Equal(product.Id, createdResult.RouteValues["id"]);

            // Verifica o objeto retornado é o produto esperado
            Assert.Equal(product, createdResult.Value);

            // Verifica que o serviço foi chamado exatamente 1 vez
            await _serviceMock.Received(1).CreateAsync(product);
        }
    }
}
