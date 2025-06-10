using eBlog.Domain.Common;
using eBlog.Domain.Interfaces;

namespace eBlog.Domain.Entities
{
    public class Product : BaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public Guid SellerId { get; set; } // User
        public string? Slug { get; set; } = null!; // SEO için
        public Guid? SeoMetadataId { get; set; }
        public Guid CategoryId { get; set; }

        // Kitap özel property'leri
        public string? Author { get; set; }
        public string? Publisher { get; set; }
        public string? ISBN { get; set; }
        public int? PublicationYear { get; set; }

        // Ürün tipi: book, magazine, etc.
        public string ProductType { get; set; } = "book";

        // Çoklu dil desteği için
        public string LanguageCode { get; set; } = "tr";

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        // Navigation
        public Category Category { get; set; } = null!;
        public User Seller { get; set; } = null!;
        public SeoMetadata? SeoMetadata { get; set; }
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<Like> Likes { get; set; } = new List<Like>();
        public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
        public ICollection<ProductOrder> ProductOrders { get; set; } = new List<ProductOrder>();
        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
        public ICollection<ProductOrder> Orders { get; set; }

    }
}
