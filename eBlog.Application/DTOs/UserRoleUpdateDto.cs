namespace eBlog.Application.DTOs
{
    public class UserRoleUpdateDto
    {
        public Guid UserId { get; set; }
        public string RoleName { get; set; } = null!;
    }
}
