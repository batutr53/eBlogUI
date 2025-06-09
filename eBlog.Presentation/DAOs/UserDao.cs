using eBlog.Domain.Entities;
using eBlog.Domain.Interfaces.DAO;
using Microsoft.Extensions.Configuration;

namespace eBlog.Persistence.DAOs
{
    public class UserDao : IUserDao
    {
        private readonly IConfiguration _configuration;
        public UserDao(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<User>> GetActiveAuthorsAsync(int count)
        {
            // Şimdilik boş
            return new List<User>();
        }
    }
}
