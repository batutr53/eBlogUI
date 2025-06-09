using eBlog.Domain.Entities;

namespace eBlog.Domain.Interfaces.DAO
{
    public interface IFollowDao
    {
        Task<List<Follow>> GetTopFollowedAuthorsAsync(int count);
        // Takip ilişkisine özel dapper metotlar
    }
}
