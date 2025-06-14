using AutoMapper;
using eBlog.Application.DTOs;
using eBlog.Application.Interfaces;
using eBlog.Domain.Entities;
using eBlog.Domain.Interfaces;
using eBlog.Domain.Interfaces.DAO;
using eBlog.Shared.Results;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace eBlog.Application.Services
{
    public class PostService : GenericService<Post, PostListDto, PostCreateDto, PostUpdateDto>, IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly ISeoMetadataRepository _seoMetadataRepository;
        private readonly IPostTagRepository _postTagRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PostService(
            IPostRepository postRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ISeoMetadataRepository seoMetadataRepository,
            IPostTagRepository postTagRepository
,
            IHttpContextAccessor httpContextAccessor) : base(postRepository, unitOfWork, mapper)
        {
            _postRepository = postRepository;
            _seoMetadataRepository = seoMetadataRepository;
            _postTagRepository = postTagRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        private string GenerateSlug(string title)
        {
            return title.ToLower().Replace(" ", "-").Replace("ı", "i").Replace("ç", "c")
                .Replace("ğ", "g").Replace("ö", "o").Replace("ş", "s").Replace("ü", "u");
        }
        public override async Task<IDataResult<PostListDto>> AddAsync(PostCreateDto dto)
        {
            try
            {
                var post = _mapper.Map<Post>(dto);
                var userIdStr = _httpContextAccessor.HttpContext?.User?.FindFirst("sub")?.Value ?? _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userIdStr))
                    throw new Exception("Kullanıcı kimliği alınamadı.");

                Guid userId = Guid.Parse(userIdStr);
                // SEO varsa ekle ve bağla
                if (dto.SeoMetadata != null)
                {
                    var seo = _mapper.Map<SeoMetadata>(dto.SeoMetadata);
                    seo.Id = Guid.NewGuid();
                    seo.CanonicalGroupId = Guid.NewGuid();
                    await _seoMetadataRepository.AddAsync(seo);
                    post.SeoMetadataId = seo.Id;
                }

                post.Id = Guid.NewGuid();
                post.Slug ??= GenerateSlug(post.Title);
                post.AuthorId = userId;
                post.UserId = userId;
              await _postRepository.AddAsync(post);

                // Etiketler
                if (dto.TagIds != null && dto.TagIds.Any())
                {
                    foreach (var tagId in dto.TagIds)
                    {
                        await _postTagRepository.AddAsync(new PostTag
                        {
                            PostId = post.Id,
                            TagId = tagId
                        });
                    }
                }

                await _unitOfWork.SaveChangesAsync();

                var resultDto = _mapper.Map<PostListDto>(post);
                return new SuccessDataResult<PostListDto>(resultDto, "Post başarıyla eklendi.");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<PostListDto>("Post eklenirken hata oluştu: " + ex.Message);
            }
        }

    }
}
