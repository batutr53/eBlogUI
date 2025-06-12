namespace eBlog.Domain.Models
{
    public class ActiveAuthor
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; } = null!;
        public int PostCount { get; set; }
    }
}
