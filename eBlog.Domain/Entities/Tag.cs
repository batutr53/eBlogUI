using eBlog.Domain.Interfaces;

namespace eBlog.Domain.Entities
{
    public class Tag : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Slug { get; set; } = null!;

        // Çoklu dil desteği için
        public string LanguageCode { get; set; } = "tr";

        public ICollection<PostTag> PostTags { get; set; } = new List<PostTag>();
    }

}
