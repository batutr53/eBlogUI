using eBlog.Domain.Common;
using eBlog.Domain.Interfaces;

namespace eBlog.Domain.Entities
{

    public class Category : BaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string Slug { get; set; } = null!; // SEO için
        public Guid? ParentCategoryId { get; set; }

        // Çoklu dil desteği için
        public string LanguageCode { get; set; } = "tr";

        // Navigation
        public Category? ParentCategory { get; set; }
        public ICollection<Category> SubCategories { get; set; } = new List<Category>();
        public ICollection<Post> Posts { get; set; } = new List<Post>();
    }
}
