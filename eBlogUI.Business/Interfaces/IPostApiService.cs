using eBlog.Application.DTOs;
using eBlog.Shared.Results;

namespace eBlogUI.Business.Interfaces
{
    public interface IPostApiService
    {
        Task<DataResult<List<PostListDto>>> GetListAsync();
        Task<DataResult<List<PostListDto>>> GetPublishedPostsAsync();
        Task<DataResult<List<PostListDto>>> GetPostsByAuthorAsync(Guid authorId);
        Task<DataResult<List<PostListDto>>> GetPostsByCategoryAsync(Guid categoryId);

        // Detay metodları  
        Task<DataResult<PostDetailDto>> GetDetailAsync(Guid id);
        Task<DataResult<PostDetailDto>> GetDetailBySlugAsync(string slug);

        // CRUD metodları
        Task<Result> CreateAsync(PostCreateDto dto);
        Task<Result> UpdateAsync(Guid id, PostUpdateDto dto);
        Task<Result> DeleteAsync(Guid id);

        // Özel metodlar
        Task<DataResult<List<PostListDto>>> SearchPostsAsync(string searchTerm);
        Task<DataResult<List<PostListDto>>> GetRecentPostsAsync(int count = 10);
        Task<DataResult<List<PostListDto>>> GetPopularPostsAsync(int count = 10);

        // SEO ve Slug metodları
        Task<DataResult<PostDetailDto>> GetPostForSeoAsync(string slug);
        Task<Result> UpdatePostSeoAsync(Guid id, SeoMetadataDto seoDto);
    }
}
