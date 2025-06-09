using eBlog.Domain.Entities;
using eBlog.Domain.Interfaces;
using eBlog.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace eBlog.Persistence.Repositories
{
    public class ProductOrderRepository : GenericRepository<ProductOrder>, IProductOrderRepository
    {
        public ProductOrderRepository(AppDbContext context) : base(context) { }

        public async Task<List<ProductOrder>> GetOrdersByBuyerIdAsync(Guid buyerId)
            => await _dbSet.Include(o => o.Product)
                           .Where(x => x.BuyerId == buyerId).ToListAsync();

        public async Task<List<ProductOrder>> GetOrdersByProductIdAsync(Guid productId)
            => await _dbSet.Include(o => o.Buyer)
                           .Where(x => x.ProductId == productId).ToListAsync();
    }
}
