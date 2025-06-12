namespace eBlog.Application.DTOs.Dashboard
{
    public class ActiveAuthorDto
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; } = null!;
        public int PostCount { get; set; }
    }
}
