using eBlog.Domain.Entities;

namespace eBlog.Domain.Interfaces
{
    public interface IPostModuleRepository : IGenericRepository<PostModule>
    {
        Task<List<PostModule>> GetModulesByPostIdAsync(Guid postId);
        Task DeleteModulesByPostIdAsync(Guid postId);
        Task AddRangeAsync(IEnumerable<PostModule> modules);
    }
}
