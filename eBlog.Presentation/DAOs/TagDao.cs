using eBlog.Domain.Entities;
using eBlog.Domain.Interfaces.DAO;
using Microsoft.Extensions.Configuration;

namespace eBlog.Persistence.DAOs
{

    public class TagDao : ITagDao
    {
        private readonly IConfiguration _configuration;
        public TagDao(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<Tag>> GetMostUsedTagsAsync(int count)
        {
            // Şimdilik boş
            return new List<Tag>();
        }
    }
}
