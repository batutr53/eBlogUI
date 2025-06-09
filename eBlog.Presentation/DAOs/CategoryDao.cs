using eBlog.Domain.Entities;
using eBlog.Domain.Interfaces.DAO;
using Microsoft.Extensions.Configuration;

namespace eBlog.Persistence.DAOs
{
    public class CategoryDao : ICategoryDao
    {
        private readonly IConfiguration _configuration;
        public CategoryDao(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<Category>> GetPopularCategoriesAsync(int count)
        {
            // Şimdilik boş
            return new List<Category>();
        }
    }
}
