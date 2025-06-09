using eBlog.Domain.Entities;

namespace eBlog.Domain.Interfaces
{
    public interface IFavoriteRepository : IGenericRepository<Favorite>
    {
        Task<List<Favorite>> GetFavoritesByUserIdAsync(Guid userId);
        Task<bool> IsFavoritedAsync(Guid userId, Guid? postId, Guid? productId, Guid? commentId);
        // Favorilere özel ekstra metotlar
    }
}
