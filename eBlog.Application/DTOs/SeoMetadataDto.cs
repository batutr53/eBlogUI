namespace eBlog.Application.DTOs
{

    public class SeoMetadataDto
    {
        public string MetaTitle { get; set; } = null!;
        public string MetaDescription { get; set; } = null!;
        public string? MetaKeywords { get; set; }
        public string? CanonicalUrl { get; set; }
        public string? OpenGraphTitle { get; set; }
        public string? OpenGraphDescription { get; set; }
        public string? OpenGraphImage { get; set; }
        public string? StructuredDataJson { get; set; }
    }
}
