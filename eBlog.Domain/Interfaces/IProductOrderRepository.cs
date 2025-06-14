using eBlog.Domain.Entities;

namespace eBlog.Domain.Interfaces
{

    public interface IProductOrderRepository : IGenericRepository<ProductOrder>
    {
        Task<List<ProductOrder>> GetOrdersByBuyerIdAsync(Guid buyerId);
        Task<List<ProductOrder>> GetOrdersByProductIdAsync(Guid productId);
        // Siparişlere özel ekstra metotlar
    }
}
