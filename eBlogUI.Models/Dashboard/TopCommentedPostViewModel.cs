namespace eBlogUI.Models.Dashboard
{
    public class TopCommentedPostViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string AuthorName { get; set; } = string.Empty;
        public int CommentCount { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? ImageUrl { get; set; }
    }
}
