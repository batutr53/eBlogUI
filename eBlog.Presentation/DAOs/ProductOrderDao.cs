using eBlog.Domain.Interfaces.DAO;
using Microsoft.Extensions.Configuration;

namespace eBlog.Persistence.DAOs
{

    public class ProductOrderDao : IProductOrderDao
    {
        private readonly IConfiguration _configuration;
        public ProductOrderDao(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<decimal> GetTotalSalesForProductAsync(Guid productId)
        {
            // Şimdilik boş
            return 0;
        }
    }
}
