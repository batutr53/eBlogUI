using eBlog.Domain.Entities;
using eBlog.Domain.Interfaces;
using eBlog.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace eBlog.Persistence.Repositories
{

    public class NotificationRepository : GenericRepository<Notification>, INotificationRepository
    {
        public NotificationRepository(AppDbContext context) : base(context) { }

        public async Task<List<Notification>> GetUnreadNotificationsByUserIdAsync(Guid userId)
            => await _dbSet.Where(x => x.UserId == userId && !x.IsRead).ToListAsync();

        public async Task MarkAllAsReadAsync(Guid userId)
        {
            var notifications = await _dbSet.Where(x => x.UserId == userId && !x.IsRead).ToListAsync();
            notifications.ForEach(n => n.IsRead = true);
        }
    }
}
