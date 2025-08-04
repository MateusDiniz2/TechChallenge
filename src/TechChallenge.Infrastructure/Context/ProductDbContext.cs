using Microsoft.EntityFrameworkCore;
using TechChallenge.Domain.Entities;

namespace TechChallenge.Infrastructure.Context
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}
