using eBlog.Domain.Entities;
using eBlog.Domain.Interfaces;
using eBlog.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace eBlog.Persistence.Repositories
{
    public class PostRepository : GenericRepository<Post>, IPostRepository
    {
        public PostRepository(AppDbContext context) : base(context) { }

        public async Task<List<Post>> GetPostsByAuthorAsync(Guid authorId)
            => await _dbSet.Where(x => x.AuthorId == authorId).ToListAsync();
    }
}
