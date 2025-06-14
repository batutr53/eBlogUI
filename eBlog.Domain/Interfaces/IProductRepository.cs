using eBlog.Domain.Entities;

namespace eBlog.Domain.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<List<Product>> GetProductsBySellerAsync(Guid sellerId);
        // Ekstra product'a özel repository metotları
    }
}
