namespace eBlog.Application.DTOs
{
    public class UserListDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
    }

    public class UserDetailDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; }
        public string? Bio { get; set; }
        public string? ProfileImageUrl { get; set; }
        public bool IsAuthor { get; set; }
        public List<string> Roles { get; set; }
    }

    public class UserCreateDto
    {
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; }
        public List<string> Roles { get; set; }
    }

    public class UserUpdateDto
    {
        public string UserName { get; set; } = null!;
        public string? Bio { get; set; }
        public string? ProfileImageUrl { get; set; }
    }
}
