using TechChallenge.Application.Interfaces;
using TechChallenge.Application.Queries;
using TechChallenge.Domain.Entities;
using TechChallenge.Domain.Interfaces;

namespace TechChallenge.Application.Handlers
{
    public class GetAllProductsHandler : IHandler<GetAllProductsQuery, IEnumerable<Product>>
    {
        private readonly IProductRepository _repository;

        public GetAllProductsHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Product>> HandleAsync(GetAllProductsQuery query)
        {
            return await _repository.GetAllAsync();
        }
    }
}
