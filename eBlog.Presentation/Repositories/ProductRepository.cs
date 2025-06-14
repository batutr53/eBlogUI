using eBlog.Domain.Entities;
using eBlog.Domain.Interfaces;
using eBlog.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace eBlog.Persistence.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context) { }

        public async Task<List<Product>> GetProductsBySellerAsync(Guid sellerId)
            => await _dbSet.Where(x => x.SellerId == sellerId).ToListAsync();
    }
}
