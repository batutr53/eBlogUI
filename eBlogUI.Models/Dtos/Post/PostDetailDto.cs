namespace eBlogUI.Models.Dtos.Post
{
    public class PostDetailDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string Summary { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string AuthorName { get; set; } = string.Empty;
        public Guid AuthorId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public Guid CategoryId { get; set; }
        public int ViewCount { get; set; }
        public int LikeCount { get; set; }
        public int CommentCount { get; set; }
        public string Slug { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public bool IsLikedByCurrentUser { get; set; }
        public List<string> Tags { get; set; } = new List<string>();
        public SeoMetadataDto? SeoMetadata { get; set; }
    }
}
