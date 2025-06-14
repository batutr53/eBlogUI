using eBlog.Domain.Entities;

namespace eBlog.Domain.Interfaces
{
    public interface IPostRepository : IGenericRepository<Post>
    {
        Task<List<Post>> GetPostsByAuthorAsync(Guid authorId);
        Task<Post?> GetPostWithDetailsAsync(Guid id);
        Task<List<Post>> GetPostsWithBasicDetailsAsync();
        Task<Post?> GetPostBySlugWithDetailsAsync(string slug);
    }
}
