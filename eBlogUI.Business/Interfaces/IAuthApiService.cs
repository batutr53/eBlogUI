using eBlog.Application.DTOs.Auth;
using eBlog.Shared.Results;

namespace eBlogUI.Business.Interfaces
{
    public interface IAuthApiService
    {
        Task<DataResult<string>> LoginAsync(LoginDto dto);
    }
}
