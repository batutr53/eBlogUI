using eBlog.Domain.Entities;
using eBlog.Domain.Interfaces;
using eBlog.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace eBlog.Persistence.Repositories
{
    public class CommentRepository : GenericRepository<Comment>, ICommentRepository
    {
        public CommentRepository(AppDbContext context) : base(context) { }

        public async Task<List<Comment>> GetCommentsByPostIdAsync(Guid postId)
            => await _dbSet.Where(x => x.PostId == postId).ToListAsync();

        public async Task<List<Comment>> GetCommentsByProductIdAsync(Guid productId)
            => await _dbSet.Where(x => x.BookId == productId).ToListAsync();
    }
}
