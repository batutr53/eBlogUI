using eBlog.Domain.Entities;
using eBlog.Domain.Interfaces;
using eBlog.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace eBlog.Persistence.Repositories
{
    public class FavoriteRepository : GenericRepository<Favorite>, IFavoriteRepository
    {
        public FavoriteRepository(AppDbContext context) : base(context) { }

        public async Task<List<Favorite>> GetFavoritesByUserIdAsync(Guid userId)
            => await _dbSet.Where(x => x.UserId == userId).ToListAsync();

        public async Task<bool> IsFavoritedAsync(Guid userId, Guid? postId, Guid? productId, Guid? commentId)
            => await _dbSet.AnyAsync(x =>
                x.UserId == userId &&
                (x.PostId == postId || x.BookId == productId || x.CommentId == commentId));
    }
}
