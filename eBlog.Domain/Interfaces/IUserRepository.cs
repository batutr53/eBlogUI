using eBlog.Domain.Entities;

namespace eBlog.Domain.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User?> GetByEmailAsync(string email);
        Task<User> GetByIdWithRolesAsync(Guid userId);
        Task<User> GetByResetTokenAsync(string resetToken);
        Task<User> GetByEmailVerificationTokenAsync(string token);
    }
}
