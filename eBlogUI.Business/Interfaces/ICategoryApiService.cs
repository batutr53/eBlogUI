using eBlog.Application.DTOs;
using eBlog.Shared.Results;

namespace eBlogUI.Business.Services.Interfaces
{
    public interface ICategoryApiService
    {
        Task<DataResult<List<CategoryListDto>>> GetListAsync();
    }
}
