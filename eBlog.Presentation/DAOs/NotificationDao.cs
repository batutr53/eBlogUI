using eBlog.Domain.Interfaces.DAO;
using Microsoft.Extensions.Configuration;

namespace eBlog.Persistence.DAOs
{
    public class NotificationDao : INotificationDao
    {
        private readonly IConfiguration _configuration;
        public NotificationDao(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<int> GetUnreadCountByUserIdAsync(Guid userId)
        {
            // Şimdilik boş
            return 0;
        }
    }
}
