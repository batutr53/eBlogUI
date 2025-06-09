using eBlog.Application.DTOs;
using eBlog.Domain.Entities;
using eBlog.Domain.Interfaces;
using eBlog.Shared.Results;

namespace eBlog.Application.Interfaces
{
    public interface ITagService : IGenericService<Tag,TagListDto, TagCreateDto, TagCreateDto>
    {
        Task<IDataResult<List<TagListDto>>> GetMostUsedTagsAsync(int count);
        Task<IDataResult<TagDetailDto>> GetBySlugAsync(string slug);

    }
}
