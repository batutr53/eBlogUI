using eBlog.Domain.Entities;

namespace eBlog.Domain.Interfaces
{
    public interface ICommentRepository : IGenericRepository<Comment>
    {
        Task<List<Comment>> GetCommentsByPostIdAsync(Guid postId);
        Task<List<Comment>> GetCommentsByProductIdAsync(Guid productId);
        // Yorumlara özel ekstra metotlar
    }
}
