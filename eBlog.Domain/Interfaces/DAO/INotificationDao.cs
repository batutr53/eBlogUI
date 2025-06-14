namespace eBlog.Domain.Interfaces.DAO
{
    public interface INotificationDao
    {
        Task<int> GetUnreadCountByUserIdAsync(Guid userId);
        // Bildirimlere özel dapper metotlar
    }
}
