using AutoMapper;
using eBlog.Application.DTOs;
using eBlog.Application.Interfaces;
using eBlog.Domain.Entities;
using eBlog.Domain.Interfaces;
using eBlog.Domain.Interfaces.DAO;
using eBlog.Shared.Results;

namespace eBlog.Application.Services
{
    public class PostService : GenericService<Post, PostListDto, PostCreateDto, PostUpdateDto>, IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IPostDao _postDao;
        private readonly IMapper _mapper;

        public PostService(
            IPostRepository postRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IPostDao postDao
        ) : base(postRepository, unitOfWork, mapper)
        {
            _postRepository = postRepository;
            _postDao = postDao;
            _mapper = mapper;
        }

        public async Task<IDataResult<List<PostListDto>>> GetRecentPostsAsync(int count)
        {
            try
            {
                var entities = await _postDao.GetRecentPostsAsync(count);
                var dtos = _mapper.Map<List<PostListDto>>(entities);
                return new SuccessDataResult<List<PostListDto>>(dtos);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<PostListDto>>("Son postlar getirilirken hata oluştu: " + ex.Message);
            }
        }
    }
}
