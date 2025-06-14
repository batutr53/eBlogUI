using eBlog.Domain.Entities;
using eBlog.Domain.Interfaces;
using eBlog.Persistence.Contexts;

namespace eBlog.Persistence.Repositories
{
    public class PostTagRepository : GenericRepository<PostTag>, IPostTagRepository
    {
        public PostTagRepository(AppDbContext context) : base(context) { }
    }

}
