using TechChallenge.Application.Interfaces;
using TechChallenge.Domain.Entities;

namespace TechChallenge.Application.Queries
{
    public class GetProductByIdQuery : IRequest<Product?>
    {
        public string Id { get; }

        public GetProductByIdQuery(string id)
        {
            Id = id;
        }
    }
}