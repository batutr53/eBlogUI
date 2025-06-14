using eBlog.Application.DTOs;
using eBlog.Domain.Entities;
using eBlog.Domain.Interfaces;
using eBlog.Shared.Results;

namespace eBlog.Application.Interfaces
{
    public interface INotificationService : IGenericService<Notification,NotificationDto, NotificationDto, NotificationDto>
    {
        Task<IDataResult<List<NotificationDto>>> GetUnreadNotificationsByUserIdAsync(Guid userId);
        Task<IResult> MarkAllAsReadAsync(Guid userId);
        Task<IDataResult<int>> GetUnreadCountByUserIdAsync(Guid userId);

    }
}
