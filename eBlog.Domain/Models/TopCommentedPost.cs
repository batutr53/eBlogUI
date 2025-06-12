namespace eBlog.Domain.Models.Dashboard
{
    public class TopCommentedPost
    {
        public Guid PostId { get; set; }
        public string Title { get; set; } = null!;
        public string Author { get; set; } = null!;
        public int CommentCount { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
