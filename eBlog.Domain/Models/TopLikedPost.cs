namespace eBlog.Domain.Models.Dashboard
{
    public class TopLikedPost
    {
        public Guid PostId { get; set; }
        public string Title { get; set; } = null!;
        public string Author { get; set; } = null!;
        public int LikeCount { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
