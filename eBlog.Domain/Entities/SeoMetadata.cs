using eBlog.Domain.Common;
using eBlog.Domain.Interfaces;

namespace eBlog.Domain.Entities
{
    public class SeoMetadata : BaseEntity
    {
        public Guid Id { get; set; }
        public Guid CanonicalGroupId { get; set; }  // SEO varyantlarını gruplayan ID

        public string MetaTitle { get; set; } = null!;
        public string MetaDescription { get; set; } = null!;
        public string? MetaKeywords { get; set; }
        public string? CanonicalUrl { get; set; }
        public string? OpenGraphTitle { get; set; }
        public string? OpenGraphDescription { get; set; }
        public string? OpenGraphImage { get; set; }
        public string? StructuredDataJson { get; set; } // JSON-LD vs.
        public string LanguageCode { get; set; }

        // İlgili Post veya Book ile bağlantı opsiyonel
        public ICollection<Post> Posts { get; set; } = new List<Post>();
        // Book ile ilişkiyi Book entity eklenince tanımlayacağız
    }
}
