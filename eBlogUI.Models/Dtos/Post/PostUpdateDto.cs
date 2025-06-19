namespace eBlogUI.Models.Dtos.Post
{
    public class PostUpdateDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string Summary { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public Guid CategoryId { get; set; }
        public string Slug { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public List<string> Tags { get; set; } = new List<string>();
        public SeoMetadataDto? SeoMetadata { get; set; }
    }
}
