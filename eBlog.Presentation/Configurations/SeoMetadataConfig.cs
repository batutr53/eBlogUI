using eBlog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eBlog.Persistence.Configurations
{
    public class SeoMetadataConfig : IEntityTypeConfiguration<SeoMetadata>
    {
        public void Configure(EntityTypeBuilder<SeoMetadata> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.MetaTitle).IsRequired().HasMaxLength(200);
            builder.Property(x => x.MetaDescription).IsRequired().HasMaxLength(500);
            builder.Property(x => x.MetaKeywords).HasMaxLength(300);
            builder.Property(x => x.CanonicalUrl).HasMaxLength(300);
            builder.Property(x => x.OpenGraphTitle).HasMaxLength(200);
            builder.Property(x => x.OpenGraphDescription).HasMaxLength(500);
            builder.Property(x => x.OpenGraphImage).HasMaxLength(500);
            builder.Property(x => x.StructuredDataJson).HasColumnType("text");
        }
    }

}
