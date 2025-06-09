using eBlog.Domain.Entities;
using eBlog.Domain.Interfaces.DAO;
using Microsoft.Extensions.Configuration;

namespace eBlog.Persistence.DAOs
{
    public class ProductDao : IProductDao
    {
        private readonly IConfiguration _configuration;
        public ProductDao(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<Product>> GetPopularProductsAsync(int count)
        {
            return new List<Product>();
        }
    }
}
