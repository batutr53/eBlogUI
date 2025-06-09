using eBlog.Domain.Entities;

namespace eBlog.Domain.Interfaces.DAO
{

    public interface IUserDao
    {
        Task<List<User>> GetActiveAuthorsAsync(int count);
        // Kullanıcıya özel dapper metotlar
    }
}
