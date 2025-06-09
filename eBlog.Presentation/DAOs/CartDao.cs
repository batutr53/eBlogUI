using eBlog.Domain.Interfaces.DAO;
using Microsoft.Extensions.Configuration;

namespace eBlog.Persistence.DAOs
{

    public class CartDao : ICartDao
    {
        private readonly IConfiguration _configuration;
        public CartDao(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<decimal> GetTotalCartPriceAsync(Guid cartId)
        {
            // Şimdilik boş
            return 0;
        }
    }
}
