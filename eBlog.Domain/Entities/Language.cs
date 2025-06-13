using eBlog.Domain.Common;

namespace eBlog.Domain.Entities
{
    public class Language : BaseEntity
    {
        public string Code { get; set; } = null!; // "tr", "en", "de"
        public string Name { get; set; } = null!; // "Türkçe", "English"
        public bool IsDefault { get; set; }

        public ICollection<Post> Posts { get; set; } = new List<Post>();
    }

}
