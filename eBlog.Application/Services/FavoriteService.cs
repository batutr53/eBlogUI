using AutoMapper;
using eBlog.Application.DTOs;
using eBlog.Application.Interfaces;
using eBlog.Domain.Entities;
using eBlog.Domain.Interfaces;
using eBlog.Domain.Interfaces.DAO;
using eBlog.Shared.Results;

namespace eBlog.Application.Services
{
    public class FavoriteService : GenericService<Favorite, FavoriteListDto, FavoriteCreateDto, FavoriteCreateDto>, IFavoriteService
    {
        private readonly IFavoriteRepository _favoriteRepository;
        private readonly IFavoriteDao _favoriteDao;
        private readonly IMapper _mapper;

        public FavoriteService(
            IFavoriteRepository favoriteRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IFavoriteDao favoriteDao
        ) : base(favoriteRepository, unitOfWork, mapper)
        {
            _favoriteRepository = favoriteRepository;
            _favoriteDao = favoriteDao;
            _mapper = mapper;
        }

        public async Task<IDataResult<List<FavoriteListDto>>> GetFavoritesByUserIdAsync(Guid userId)
        {
            try
            {
                var entities = await _favoriteRepository.GetFavoritesByUserIdAsync(userId);
                var dtos = _mapper.Map<List<FavoriteListDto>>(entities);
                return new SuccessDataResult<List<FavoriteListDto>>(dtos);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<FavoriteListDto>>("Favoriler alınırken hata oluştu: " + ex.Message);
            }
        }

        public async Task<IDataResult<bool>> IsFavoritedAsync(Guid userId, Guid? postId, Guid? productId, Guid? commentId)
        {
            try
            {
                var result = await _favoriteRepository.IsFavoritedAsync(userId, postId, productId, commentId);
                return new SuccessDataResult<bool>(result);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<bool>("Favori kontrolü sırasında hata oluştu: " + ex.Message);
            }
        }

        public async Task<IDataResult<List<FavoriteListDto>>> GetMostFavoritedPostsAsync(int count)
        {
            try
            {
                var entities = await _favoriteDao.GetMostFavoritedPostsAsync(count);
                var dtos = _mapper.Map<List<FavoriteListDto>>(entities);
                return new SuccessDataResult<List<FavoriteListDto>>(dtos);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<FavoriteListDto>>("En çok favorilenenler alınırken hata oluştu: " + ex.Message);
            }
        }
    }
}
