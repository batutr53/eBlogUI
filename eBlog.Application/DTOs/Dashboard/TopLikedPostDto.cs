namespace eBlog.Application.DTOs.Dashboard
{
    public class TopLikedPostDto
    {
        public Guid PostId { get; set; }
        public string Title { get; set; } = null!;
        public string Author { get; set; } = null!;
        public int LikeCount { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
