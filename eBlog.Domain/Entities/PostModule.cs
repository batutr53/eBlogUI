using eBlog.Domain.Common;
using eBlog.Domain.Enums;

namespace eBlog.Domain.Entities
{
    public class PostModule : BaseEntity
    {
        public Guid PostId { get; set; }
        public Post Post { get; set; }

        public ModuleType Type { get; set; } // Text, Image, Video, Quote, etc.
        public string? Content { get; set; } // Text veya Quote içerikleri
        public string? MediaUrl { get; set; } // Image veya Video URL
        public int Order { get; set; } // Sıralama
    }
}
