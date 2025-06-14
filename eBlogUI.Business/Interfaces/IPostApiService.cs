using eBlog.Application.DTOs;
using eBlog.Shared.Results;

namespace eBlogUI.Business.Interfaces
{
    public interface IPostApiService
    {
        Task<DataResult<List<PostListDto>>> GetListAsync();
        Task<DataResult<PostDetailDto>> GetDetailAsync(Guid id);
        Task<Result> CreateAsync(PostCreateDto dto);
        Task<Result> UpdateAsync(Guid id, PostUpdateDto dto);
        Task<Result> DeleteAsync(Guid id);
    }
}
