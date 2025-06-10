using eBlog.Application.DTOs;

namespace eBlog.Application.Interfaces
{
    public interface IJwtService
    {
        string GenerateJwtToken(UserDetailDto user);
        string GenerateRefreshToken();

    }
}
