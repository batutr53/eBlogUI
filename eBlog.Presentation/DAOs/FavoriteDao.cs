using eBlog.Domain.Entities;
using eBlog.Domain.Interfaces.DAO;
using Microsoft.Extensions.Configuration;

namespace eBlog.Persistence.DAOs
{
    public class FavoriteDao : IFavoriteDao
    {
        private readonly IConfiguration _configuration;
        public FavoriteDao(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<Favorite>> GetMostFavoritedPostsAsync(int count)
        {
            // Şimdilik boş
            return new List<Favorite>();
        }
    }
}
