using eBlog.Domain.Entities;

namespace eBlog.Domain.Interfaces
{
    public interface INotificationRepository : IGenericRepository<Notification>
    {
        Task<List<Notification>> GetUnreadNotificationsByUserIdAsync(Guid userId);
        Task MarkAllAsReadAsync(Guid userId);
        // Bildirimlere özel ekstra metotlar
    }
}
