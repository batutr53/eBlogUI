using eBlog.Domain.Interfaces;

namespace eBlog.Domain.Entities
{
    public class SeoMetadata : IEntity
    {
        public Guid Id { get; set; }
        public string MetaTitle { get; set; } = null!;
        public string MetaDescription { get; set; } = null!;
        public string? MetaKeywords { get; set; }
        public string? CanonicalUrl { get; set; }
        public string? OpenGraphTitle { get; set; }
        public string? OpenGraphDescription { get; set; }
        public string? OpenGraphImage { get; set; }
        public string? StructuredDataJson { get; set; } // JSON-LD vs.

        // İlgili Post veya Book ile bağlantı opsiyonel
        public ICollection<Post> Posts { get; set; } = new List<Post>();
        // Book ile ilişkiyi Book entity eklenince tanımlayacağız
    }
}
