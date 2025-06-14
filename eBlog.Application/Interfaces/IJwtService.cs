using eBlog.Application.DTOs;
using System.Security.Claims;

namespace eBlog.Application.Interfaces
{
    public interface IJwtService
    {
        string GenerateJwtToken(UserDetailDto user);
        string GenerateRefreshToken();
        ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);


    }
}
