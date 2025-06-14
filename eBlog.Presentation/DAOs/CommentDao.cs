using eBlog.Domain.Entities;
using eBlog.Domain.Interfaces.DAO;
using Microsoft.Extensions.Configuration;

namespace eBlog.Persistence.DAOs
{
    public class CommentDao : ICommentDao
    {
        private readonly IConfiguration _configuration;
        public CommentDao(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<Comment>> GetRecentCommentsAsync(int count)
        {
            // Şimdilik boş
            return new List<Comment>();
        }
    }
}
