namespace eBlogUI.Models.Dtos.Post
{
    public class PostListDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Summary { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public DateTime CreatedDate { get; set; }
        public string AuthorName { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public int ViewCount { get; set; }
        public int LikeCount { get; set; }
        public int CommentCount { get; set; }
        public string Slug { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public List<string> Tags { get; set; } = new List<string>();
    }
}
