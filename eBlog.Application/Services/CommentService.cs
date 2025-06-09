using AutoMapper;
using eBlog.Application.DTOs;
using eBlog.Application.Interfaces;
using eBlog.Domain.Entities;
using eBlog.Domain.Interfaces;
using eBlog.Domain.Interfaces.DAO;
using eBlog.Shared.Results;

namespace eBlog.Application.Services
{
    public class CommentService : GenericService<Comment, CommentListDto, CommentCreateDto, CommentCreateDto>, ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly ICommentDao _commentDao;
        private readonly IMapper _mapper;

        public CommentService(
            ICommentRepository commentRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ICommentDao commentDao
        ) : base(commentRepository, unitOfWork, mapper)
        {
            _commentRepository = commentRepository;
            _commentDao = commentDao;
            _mapper = mapper;
        }

        public async Task<IDataResult<List<CommentListDto>>> GetCommentsByPostIdAsync(Guid postId)
        {
            try
            {
                var entities = await _commentRepository.GetCommentsByPostIdAsync(postId);
                var dtos = _mapper.Map<List<CommentListDto>>(entities);
                return new SuccessDataResult<List<CommentListDto>>(dtos);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<CommentListDto>>("Post yorumları getirilirken hata oluştu: " + ex.Message);
            }
        }

        public async Task<IDataResult<List<CommentListDto>>> GetCommentsByProductIdAsync(Guid productId)
        {
            try
            {
                var entities = await _commentRepository.GetCommentsByProductIdAsync(productId);
                var dtos = _mapper.Map<List<CommentListDto>>(entities);
                return new SuccessDataResult<List<CommentListDto>>(dtos);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<CommentListDto>>("Ürün yorumları getirilirken hata oluştu: " + ex.Message);
            }
        }

        public async Task<IDataResult<List<CommentListDto>>> GetRecentCommentsAsync(int count)
        {
            try
            {
                var entities = await _commentDao.GetRecentCommentsAsync(count);
                var dtos = _mapper.Map<List<CommentListDto>>(entities);
                return new SuccessDataResult<List<CommentListDto>>(dtos);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<CommentListDto>>("Son yorumlar getirilirken hata oluştu: " + ex.Message);
            }
        }
    }
}
