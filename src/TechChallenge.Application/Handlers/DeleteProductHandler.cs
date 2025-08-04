using TechChallenge.Application.Commands;
using TechChallenge.Application.Interfaces;
using TechChallenge.Domain.Interfaces;

namespace TechChallenge.Application.Handlers
{
    public class DeleteProductHandler : IHandler<DeleteProductCommand, bool>
    {
        private readonly IProductRepository _repository;

        public DeleteProductHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> HandleAsync(DeleteProductCommand command)
        {
            var existingProduct = await _repository.GetByIdAsync(command.Id);
            if (existingProduct == null)
                return false;

            await _repository.DeleteAsync(command.Id);
            return true;
        }
    }
}
