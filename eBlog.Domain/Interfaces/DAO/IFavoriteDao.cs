using eBlog.Domain.Entities;

namespace eBlog.Domain.Interfaces.DAO
{
    public interface IFavoriteDao
    {
        Task<List<Favorite>> GetMostFavoritedPostsAsync(int count);
        // Favorilere özel dapper metotlar
    }
}
