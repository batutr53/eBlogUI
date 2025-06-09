using eBlog.Domain.Entities;

namespace eBlog.Domain.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User?> GetByEmailAsync(string email);
        // Kullanıcıya özel başka metotlar da eklenebilir.
    }
}
