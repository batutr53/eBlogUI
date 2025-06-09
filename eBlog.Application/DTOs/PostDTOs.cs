namespace eBlog.Application.DTOs
{
    public class PostListDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string Slug { get; set; } = null!;
        public string AuthorUserName { get; set; } = null!;
    }

    public class PostDetailDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string Slug { get; set; } = null!;
        public string AuthorUserName { get; set; } = null!;
        public string CategoryName { get; set; } = null!;
        public SeoMetadataDto? SeoMetadata { get; set; }
    }

    public class PostCreateDto
    {
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public Guid CategoryId { get; set; }
        public List<Guid>? TagIds { get; set; }
    }

    public class PostUpdateDto
    {
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public Guid CategoryId { get; set; }
        public List<Guid>? TagIds { get; set; }
    }

}
