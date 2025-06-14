using eBlog.Domain.Entities;
using eBlog.Domain.Interfaces.DAO;
using Microsoft.Extensions.Configuration;

namespace eBlog.Persistence.DAOs
{

    public class PostDao : IPostDao
    {
        private readonly IConfiguration _configuration;
        public PostDao(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<Post>> GetRecentPostsAsync(int count)
        {
            // Şimdilik boş
            return new List<Post>();
        }
    }
}
