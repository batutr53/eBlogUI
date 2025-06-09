using eBlog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eBlog.Persistence.Configurations
{

    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(300);
            builder.Property(x => x.Price).HasPrecision(18, 2);
            builder.Property(x => x.Slug).IsRequired().HasMaxLength(300);
            builder.Property(x => x.ProductType).IsRequired().HasMaxLength(50);

            builder.HasOne(x => x.Seller)
                .WithMany(u => u.Product) // Not: User.Books => User.Products olarak istersen rename et
                .HasForeignKey(x => x.SellerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.SeoMetadata)
                .WithMany()
                .HasForeignKey(x => x.SeoMetadataId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(x => x.Comments)
                .WithOne(c => c.Product)
                .HasForeignKey(c => c.BookId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(x => x.LanguageCode);
        }
    }
}
