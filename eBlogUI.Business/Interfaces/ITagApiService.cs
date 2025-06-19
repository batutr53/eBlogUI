using eBlogUI.Models.Dtos;
using eBlogUI.Models.Dtos.Tag;
using eBlog.Shared.Results;

namespace eBlogUI.Business.Interfaces
{
    public interface ITagApiService
    {
        Task<IDataResult<List<TagListDto>>> GetListAsync();
        Task<IDataResult<TagListDto>> GetByIdAsync(Guid id);
        Task<IDataResult<TagListDto>> GetBySlugAsync(string slug);
        Task<IResult> CreateAsync(TagCreateDto dto);
        Task<IResult> UpdateAsync(TagUpdateDto dto);
        Task<IResult> DeleteAsync(Guid id);
        Task<IDataResult<int>> GetPostCountByTagAsync(Guid tagId);
        Task<IDataResult<List<TagListDto>>> GetPopularTagsAsync(int count = 10);
    }
}
