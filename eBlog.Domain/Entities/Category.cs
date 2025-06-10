using eBlog.Domain.Common;
using eBlog.Domain.Enums;
using eBlog.Domain.Interfaces;

namespace eBlog.Domain.Entities
{

    public class Category : BaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string Slug { get; set; } = null!; // SEO için
        public bool IsActive { get; set; }
        public ModuleType Type { get; set; }
        public Guid? ParentCategoryId { get; set; }

        // Çoklu dil desteği için
        public string LanguageCode { get; set; } = "tr";

        // Navigation
        public Category? ParentCategory { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();
        public ICollection<Category> SubCategories { get; set; } = new List<Category>();
        public ICollection<Post> Posts { get; set; } = new List<Post>();
    }
}
