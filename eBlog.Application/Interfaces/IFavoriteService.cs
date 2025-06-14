using eBlog.Application.DTOs;
using eBlog.Domain.Entities;
using eBlog.Domain.Interfaces;
using eBlog.Shared.Results;

namespace eBlog.Application.Interfaces
{
    public interface IFavoriteService : IGenericService<Favorite,FavoriteListDto, FavoriteCreateDto, FavoriteCreateDto>
    {
        Task<IDataResult<List<FavoriteListDto>>> GetFavoritesByUserIdAsync(Guid userId);
        Task<IDataResult<bool>> IsFavoritedAsync(Guid userId, Guid? postId, Guid? productId, Guid? commentId);
        Task<IDataResult<List<FavoriteListDto>>> GetMostFavoritedPostsAsync(int count);

    }
}
