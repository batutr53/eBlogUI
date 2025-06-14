using eBlog.Application.DTOs;
using eBlog.Shared.Results;

namespace eBlogUI.Business.Services.Interfaces
{
    public interface ITagApiService
    {
        Task<DataResult<List<TagListDto>>> GetListAsync();
    }
}
