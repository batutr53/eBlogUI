using eBlogUI.Models.Dtos;
using eBlog.Shared.Results;

namespace eBlogUI.Business.Interfaces
{
    public interface IAuthApiService
    {
        Task<IDataResult<AuthUserDto>> LoginAsync(LoginDto dto);
        Task<IResult> RegisterAsync(RegisterDto dto);
        Task<IResult> LogoutAsync();
        Task<IDataResult<AuthUserDto>> GetCurrentUserAsync();
    }
}
