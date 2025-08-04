using TechChallenge.Domain.Entities;
using TechChallenge.Domain.Interfaces;
using TechChallenge.Application.Interfaces;
using TechChallenge.Application.Queries;

namespace TechChallenge.Application.Handlers
{
    public class GetProductByIdHandler : IHandler<GetProductByIdQuery, Product?>
    {
        private readonly IProductRepository _repository;

        public GetProductByIdHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<Product?> HandleAsync(GetProductByIdQuery query)
        {
            return await _repository.GetByIdAsync(query.Id);
        }
    }
}
