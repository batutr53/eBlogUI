using eBlog.Domain.Entities;
using eBlog.Domain.Interfaces.DAO;
using Microsoft.Extensions.Configuration;

namespace eBlog.Persistence.DAOs
{
    public class FollowDao : IFollowDao
    {
        private readonly IConfiguration _configuration;
        public FollowDao(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<Follow>> GetTopFollowedAuthorsAsync(int count)
        {
            // Şimdilik boş
            return new List<Follow>();
        }
    }
}
