namespace eBlog.Application.DTOs.Auth
{
    public class UserRoleUpdateDto
    {
        public Guid UserId { get; set; }
        public string RoleName { get; set; }
    }
}
