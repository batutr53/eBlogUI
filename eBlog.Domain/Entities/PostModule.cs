using eBlog.Domain.Common;
using eBlog.Domain.Enums;

namespace eBlog.Domain.Entities
{
    public class PostModule : BaseEntity
    {
        public Guid PostId { get; set; }
        public Post Post { get; set; }
        public int SortOrder { get; set; }

        public ModuleType Type { get; set; }
        public string? Content { get; set; }
        public string? MediaUrl { get; set; }
        public int Order { get; set; }
        public Guid? TagId { get; set; }
        public ICollection<Tag> Tags { get; set; }
    }
}
