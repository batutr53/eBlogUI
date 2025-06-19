using eBlogUI.Models.Dtos;
using eBlogUI.Models.Dtos.Post;
using eBlog.Shared.Results;

namespace eBlogUI.Business.Interfaces
{
    public interface IPostApiService
    {
        Task<IDataResult<List<PostListDto>>> GetListAsync();
        Task<IDataResult<List<PostListDto>>> GetPublishedPostsAsync();
        Task<IDataResult<List<PostListDto>>> GetPostsByAuthorAsync(Guid authorId);
        Task<IDataResult<List<PostListDto>>> GetPostsByCategoryAsync(Guid categoryId);

        // Detay metodları  
        Task<IDataResult<PostDetailDto>> GetDetailAsync(Guid id);
        Task<IDataResult<PostDetailDto>> GetDetailBySlugAsync(string slug);

        // CRUD metodları
        Task<IResult> CreateAsync(PostCreateDto dto);
        Task<IResult> UpdateAsync(Guid id, PostUpdateDto dto);
        Task<IResult> DeleteAsync(Guid id);

        // Özel metodlar
        Task<IDataResult<List<PostListDto>>> SearchPostsAsync(string searchTerm);
        Task<IDataResult<List<PostListDto>>> GetRecentPostsAsync(int count = 10);
        Task<IDataResult<List<PostListDto>>> GetPopularPostsAsync(int count = 10);

        // SEO ve Slug metodları
        Task<IDataResult<PostDetailDto>> GetPostForSeoAsync(string slug);
        Task<IResult> UpdatePostSeoAsync(Guid id, SeoMetadataDto seoDto);
    }
}
