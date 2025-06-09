using eBlog.Domain.Entities;

namespace eBlog.Domain.Interfaces.DAO
{
    public interface IPostDao
    {
        Task<List<Post>> GetRecentPostsAsync(int count);
        // Dapper özel metotlar
    }
}
