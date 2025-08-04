using TechChallenge.Application.Commands;
using TechChallenge.Application.Interfaces;
using TechChallenge.Domain.Interfaces;

namespace TechChallenge.Application.Handlers
{
    public class UpdateProductHandler : IHandler<UpdateProductCommand, bool>
    {
        private readonly IProductRepository _repository;

        public UpdateProductHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> HandleAsync(UpdateProductCommand command)
        {
            var existingProduct = await _repository.GetByIdAsync(command.Id);
            if (existingProduct == null)
                return false;

            existingProduct.UpdateDetails(command.Name, command.Description, command.Price);
            existingProduct.UpdateStock(command.StockQuantity);

            await _repository.UpdateAsync(existingProduct);

            return true;
        }
    }
}
