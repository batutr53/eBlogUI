using eBlog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eBlog.Persistence.Configurations
{

    public class PostConfig : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Title).IsRequired().HasMaxLength(300);
            builder.Property(x => x.Slug).IsRequired().HasMaxLength(300);

            builder.HasOne(x => x.Author)
                .WithMany(u => u.Posts)
                .HasForeignKey(x => x.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Category)
                .WithMany(c => c.Posts)
                .HasForeignKey(x => x.CategoryId);

            builder.HasOne(x => x.SeoMetadata)
                .WithMany(m => m.Posts)
                .HasForeignKey(x => x.SeoMetadataId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(x => x.Comments)
                .WithOne(c => c.Post)
                .HasForeignKey(c => c.PostId);

            builder.HasMany(x => x.Likes)
                .WithOne(l => l.Post)
                .HasForeignKey(l => l.PostId);

            builder.HasMany(x => x.Favorites)
                .WithOne(f => f.Post)
                .HasForeignKey(f => f.PostId);

            // Çoklu dil için index
            builder.HasIndex(x => x.LanguageCode);
        }
    }
}
