using eBlog.Application.DTOs;
using eBlog.Application.DTOs.Auth;
using eBlog.Domain.Entities;
using eBlog.Domain.Interfaces;
using eBlog.Shared.Results;

namespace eBlog.Application.Interfaces
{
    public interface IUserService : IGenericService<User,UserListDto, UserCreateDto, UserUpdateDto>
    {
        Task<IDataResult<UserDetailDto>> GetByEmailAsync(string email);
        Task<IDataResult<List<UserListDto>>> GetActiveAuthorsAsync(int count);
        Task<IResult> AddRoleToUserAsync(UserRoleUpdateDto dto);
        Task<IResult> RemoveRoleFromUserAsync(UserRoleUpdateDto dto);
        Task<IDataResult<string>> UpdateRolesAndGenerateJwtAsync(Guid userId, List<string> roles);
        Task<IResult> SaveRefreshTokenAsync(Guid userId, string refreshToken, string ipAddress);
        Task<IResult> GeneratePasswordResetTokenAsync(string email);
        Task<IResult> ResetPasswordAsync(string token, string newPassword);
        Task<IResult> VerifyEmailAsync(string token);

    }
}
