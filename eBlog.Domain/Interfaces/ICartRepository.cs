using eBlog.Domain.Entities;

namespace eBlog.Domain.Interfaces
{
    public interface ICartRepository : IGenericRepository<Cart>
    {
        Task<Cart?> GetCartByUserIdAsync(Guid userId);
        Task<Cart?> GetByUserIdWithItemsAsync(Guid userId);
    }
}
