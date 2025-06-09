using eBlog.Domain.Entities;

namespace eBlog.Domain.Interfaces
{
    public interface IPostRepository : IGenericRepository<Post>
    {
        Task<List<Post>> GetPostsByAuthorAsync(Guid authorId);
        // Özel metotlar
    }
}
