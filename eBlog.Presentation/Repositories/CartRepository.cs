﻿using eBlog.Domain.Entities;
using eBlog.Domain.Interfaces;
using eBlog.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace eBlog.Persistence.Repositories
{
    public class CartRepository : GenericRepository<Cart>, ICartRepository
    {
        public CartRepository(AppDbContext context) : base(context) { }

        public async Task<Cart?> GetCartByUserIdAsync(Guid userId)
            => await _dbSet.Include(x => x.CartItems).ThenInclude(ci => ci.Product)
                           .FirstOrDefaultAsync(x => x.UserId == userId);

        public Task<Cart?> GetByUserIdWithItemsAsync(Guid userId)
        {
            return _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);
        }

    }
}
