namespace eBlog.Application.DTOs
{

    // Listeleme için
    public class ProductListDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string ProductType { get; set; } = null!;
        public string SellerUserName { get; set; } = null!;
    }

    // Detay için
    public class ProductDetailDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string ProductType { get; set; } = null!;
        public string? Author { get; set; }
        public string? Publisher { get; set; }
        public string? ISBN { get; set; }
        public int? PublicationYear { get; set; }
        public string SellerUserName { get; set; } = null!;
        public SeoMetadataDto? SeoMetadata { get; set; }
    }

    // Oluşturma için
    public class ProductCreateDto
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string ProductType { get; set; } = "book";
        public string? Author { get; set; }
        public string? Publisher { get; set; }
        public string? ISBN { get; set; }
        public int? PublicationYear { get; set; }
    }

    // Güncelleme için
    public class ProductUpdateDto
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string ProductType { get; set; } = "book";
        public string? Author { get; set; }
        public string? Publisher { get; set; }
        public string? ISBN { get; set; }
        public int? PublicationYear { get; set; }
    }
}
