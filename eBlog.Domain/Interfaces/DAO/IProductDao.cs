using eBlog.Domain.Entities;

namespace eBlog.Domain.Interfaces.DAO
{
    public interface IProductDao
    {
        Task<List<Product>> GetPopularProductsAsync(int count);
    }
}
