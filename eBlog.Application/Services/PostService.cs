using AutoMapper;
using eBlog.Application.DTOs;
using eBlog.Application.Interfaces;
using eBlog.Domain.Entities;
using eBlog.Domain.Interfaces;
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
            IPostTagRepository postTagRepository,
            IHttpContextAccessor httpContextAccessor) : base(postRepository, unitOfWork, mapper)
        {
            _postRepository = postRepository;
            _seoMetadataRepository = seoMetadataRepository;
            _postTagRepository = postTagRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        // ✅ OVERRIDE: GetByIdAsync metodunu override et
        public override async Task<IDataResult<PostListDto>> GetByIdAsync(Guid id)
        {
            try
            {
                // 🎯 Include'li metodu kullan
                var post = await _postRepository.GetPostWithDetailsAsync(id);

                if (post == null)
                    return new ErrorDataResult<PostListDto>("Post bulunamadı.");

                var dto = _mapper.Map<PostListDto>(post);
                return new SuccessDataResult<PostListDto>(dto);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<PostListDto>($"Post getirilirken hata oluştu: {ex.Message}");
            }
        }

        // ✅ YENİ: Post detayı için özel metod
        public async Task<IDataResult<PostDetailDto>> GetPostDetailAsync(Guid id)
        {
            try
            {
                var post = await _postRepository.GetPostWithDetailsAsync(id);

                if (post == null)
                    return new ErrorDataResult<PostDetailDto>("Post bulunamadı.");

                var dto = _mapper.Map<PostDetailDto>(post);

                // 🎯 Tag ID'lerini manuel set et (eğer mapping problemi varsa)
                if (post.PostTags?.Any() == true)
                {
                    dto.TagIds = post.PostTags.Select(pt => pt.TagId).ToList();
                }

                return new SuccessDataResult<PostDetailDto>(dto, "Post detayı başarıyla getirildi.");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<PostDetailDto>($"Post detayı getirilirken hata oluştu: {ex.Message}");
            }
        }

        // ✅ YENİ: Slug ile detay
        public async Task<IDataResult<PostDetailDto>> GetPostBySlugAsync(string slug)
        {
            try
            {
                var post = await _postRepository.GetPostBySlugWithDetailsAsync(slug);

                if (post == null)
                    return new ErrorDataResult<PostDetailDto>("Post bulunamadı.");

                var dto = _mapper.Map<PostDetailDto>(post);
                return new SuccessDataResult<PostDetailDto>(dto);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<PostDetailDto>($"Post getirilirken hata oluştu: {ex.Message}");
            }
        }

        // ✅ OVERRIDE: Liste metodunu da optimize et
        public override async Task<IDataResult<List<PostListDto>>> GetAllAsync()
        {
            try
            {
                var posts = await _postRepository.GetPostsWithBasicDetailsAsync();
                var dtos = _mapper.Map<List<PostListDto>>(posts);
                return new SuccessDataResult<List<PostListDto>>(dtos);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<PostListDto>>($"Post listesi alınırken hata oluştu: {ex.Message}");
            }
        }

        private string GenerateSlug(string title)
        {
            return title.ToLower().Replace(" ", "-").Replace("ı", "i").Replace("ç", "c")
                .Replace("ğ", "g").Replace("ö", "o").Replace("ş", "s").Replace("ü", "u");
        }

        public async Task<IDataResult<PostListDto>> AddAsync(PostCreateDto dto)
        {
            try
            {
                var post = _mapper.Map<Post>(dto);
                var userIdStr = _httpContextAccessor.HttpContext?.User?.FindFirst("sub")?.Value ??
                               _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

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
                post.CreatedAt = DateTime.UtcNow;
                post.PublishedAt = DateTime.UtcNow;
                post.IsPublished = true;

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