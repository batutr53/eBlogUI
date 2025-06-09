using AutoMapper;
using eBlog.Application.DTOs;
using eBlog.Application.Interfaces;
using eBlog.Domain.Entities;
using eBlog.Domain.Interfaces;
using eBlog.Domain.Interfaces.DAO;
using eBlog.Shared.Results;

namespace eBlog.Application.Services
{
    public class FollowService : GenericService<Follow, FollowListDto, FollowCreateDto, FollowCreateDto>, IFollowService
    {
        private readonly IFollowRepository _followRepository;
        private readonly IFollowDao _followDao;
        private readonly IMapper _mapper;

        public FollowService(
            IFollowRepository followRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IFollowDao followDao
        ) : base(followRepository, unitOfWork, mapper)
        {
            _followRepository = followRepository;
            _followDao = followDao;
            _mapper = mapper;
        }

        public async Task<IDataResult<List<FollowListDto>>> GetFollowersAsync(Guid userId)
        {
            try
            {
                var entities = await _followRepository.GetFollowersAsync(userId);
                var dtos = _mapper.Map<List<FollowListDto>>(entities);
                return new SuccessDataResult<List<FollowListDto>>(dtos);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<FollowListDto>>("Takipçiler getirilirken hata oluştu: " + ex.Message);
            }
        }

        public async Task<IDataResult<List<FollowListDto>>> GetFollowingsAsync(Guid userId)
        {
            try
            {
                var entities = await _followRepository.GetFollowingsAsync(userId);
                var dtos = _mapper.Map<List<FollowListDto>>(entities);
                return new SuccessDataResult<List<FollowListDto>>(dtos);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<FollowListDto>>("Takip edilenler getirilirken hata oluştu: " + ex.Message);
            }
        }

        public async Task<IDataResult<bool>> IsFollowingAsync(Guid followerId, Guid followingId)
        {
            try
            {
                var result = await _followRepository.IsFollowingAsync(followerId, followingId);
                return new SuccessDataResult<bool>(result);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<bool>("Takip durumu sorgulanırken hata oluştu: " + ex.Message);
            }
        }

        public async Task<IDataResult<List<FollowListDto>>> GetTopFollowedAuthorsAsync(int count)
        {
            try
            {
                var entities = await _followDao.GetTopFollowedAuthorsAsync(count);
                var dtos = _mapper.Map<List<FollowListDto>>(entities);
                return new SuccessDataResult<List<FollowListDto>>(dtos);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<FollowListDto>>("En çok takip edilen yazarlar alınırken hata oluştu: " + ex.Message);
            }
        }
    }
}
