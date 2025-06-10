using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using eBlog.Domain.Entities;

namespace eBlog.Persistence.Configurations
{
    public class PostConfig : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.Slug)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.CreatedAt)
                .HasDefaultValueSql("NOW()");

            builder.Property(p => p.IsPublished)
                .HasDefaultValue(false);

            builder.HasIndex(p => p.Slug)
                .IsUnique();

            builder.HasOne(p => p.User)
                   .WithMany(u => u.Posts)
                   .HasForeignKey(p => p.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Category)
                   .WithMany(c => c.Posts)
                   .HasForeignKey(p => p.CategoryId)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(p => p.Comments)
                   .WithOne(c => c.Post)
                   .HasForeignKey(c => c.PostId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.Tags)
                   .WithMany(t => t.Posts)
                   .UsingEntity<PostTag>(
                       j => j.HasOne(pt => pt.Tag).WithMany().HasForeignKey(pt => pt.TagId),
                       j => j.HasOne(pt => pt.Post).WithMany().HasForeignKey(pt => pt.PostId),
                       j =>
                       {
                           j.HasKey(pt => new { pt.PostId, pt.TagId });
                           j.ToTable("PostTags");
                       });
        }
    }
}
