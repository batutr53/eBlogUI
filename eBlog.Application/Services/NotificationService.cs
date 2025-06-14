using AutoMapper;
using eBlog.Application.DTOs;
using eBlog.Application.Interfaces;
using eBlog.Domain.Entities;
using eBlog.Domain.Interfaces;
using eBlog.Domain.Interfaces.DAO;
using eBlog.Shared.Results;

namespace eBlog.Application.Services
{
    public class NotificationService : GenericService<Notification, NotificationDto, NotificationDto, NotificationDto>, INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly INotificationDao _notificationDao;
        private readonly IMapper _mapper;

        public NotificationService(
            INotificationRepository notificationRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            INotificationDao notificationDao
        ) : base(notificationRepository, unitOfWork, mapper)
        {
            _notificationRepository = notificationRepository;
            _notificationDao = notificationDao;
            _mapper = mapper;
        }

        public async Task<IDataResult<List<NotificationDto>>> GetUnreadNotificationsByUserIdAsync(Guid userId)
        {
            try
            {
                var entities = await _notificationRepository.GetUnreadNotificationsByUserIdAsync(userId);
                var dtos = _mapper.Map<List<NotificationDto>>(entities);
                return new SuccessDataResult<List<NotificationDto>>(dtos);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<NotificationDto>>("Okunmamış bildirimler getirilirken hata oluştu: " + ex.Message);
            }
        }

        public async Task<IResult> MarkAllAsReadAsync(Guid userId)
        {
            try
            {
                await _notificationRepository.MarkAllAsReadAsync(userId);
                await _unitOfWork.SaveChangesAsync();
                return new SuccessResult("Tüm bildirimler okundu olarak işaretlendi.");
            }
            catch (Exception ex)
            {
                return new ErrorResult("Bildirimler okundu yapılırken hata oluştu: " + ex.Message);
            }
        }

        public async Task<IDataResult<int>> GetUnreadCountByUserIdAsync(Guid userId)
        {
            try
            {
                var count = await _notificationDao.GetUnreadCountByUserIdAsync(userId);
                return new SuccessDataResult<int>(count);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<int>("Okunmamış bildirim sayısı alınamadı: " + ex.Message);
            }
        }
    }
}
