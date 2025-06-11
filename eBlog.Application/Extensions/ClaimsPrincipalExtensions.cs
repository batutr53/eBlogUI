using System.Security.Claims;

namespace eBlog.Application.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal user)
        {
            var userIdStr = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrWhiteSpace(userIdStr))
                throw new UnauthorizedAccessException("Kullanıcı kimliği bulunamadı.");

            return Guid.Parse(userIdStr);
        }
    }
}
