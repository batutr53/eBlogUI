using eBlog.Domain.Entities;
using eBlog.Domain.Interfaces;
using eBlog.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace eBlog.Persistence.Repositories
{

    public class FollowRepository : GenericRepository<Follow>, IFollowRepository
    {
        public FollowRepository(AppDbContext context) : base(context) { }

        public async Task<List<Follow>> GetFollowersAsync(Guid userId)
            => await _dbSet.Where(x => x.FollowingId == userId).ToListAsync();

        public async Task<List<Follow>> GetFollowingsAsync(Guid userId)
            => await _dbSet.Where(x => x.FollowerId == userId).ToListAsync();

        public async Task<bool> IsFollowingAsync(Guid followerId, Guid followingId)
            => await _dbSet.AnyAsync(x => x.FollowerId == followerId && x.FollowingId == followingId);
    }
}
