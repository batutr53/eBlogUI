using eBlog.Domain.Entities;

namespace eBlog.Domain.Interfaces
{
    public interface IFollowRepository : IGenericRepository<Follow>
    {
        Task<List<Follow>> GetFollowersAsync(Guid userId);
        Task<List<Follow>> GetFollowingsAsync(Guid userId);
        Task<bool> IsFollowingAsync(Guid followerId, Guid followingId);
        // Takip ilişkisine özel metotlar
    }
}
