using Microsoft.AspNetCore.Mvc;
using TechChallenge.Application.Commands;
using TechChallenge.Application.Interfaces;
using TechChallenge.Application.Queries;

namespace TechChallenge.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IDispatcher _dispatcher;

        public ProductController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var query = new GetAllProductsQuery();
            var products = await _dispatcher.SendAsync(query);
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var query = new GetProductByIdQuery(id);
            var product = await _dispatcher.SendAsync(query);
            if (product == null)
                return NotFound();
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateProductCommand command)
        {
            var product = await _dispatcher.SendAsync(command);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] UpdateProductCommand command)
        {
            if (id != command.Id)
                return BadRequest("Id da rota diferente do id do comando.");

            var updated = await _dispatcher.SendAsync(command);
            return updated ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var command = new DeleteProductCommand(id);
            var deleted = await _dispatcher.SendAsync(command);
            return deleted ? NoContent() : NotFound();
        }
    }
}
