using eBlog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eBlog.Persistence.Configurations
{
    public class CommentConfig : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Text)
                   .IsRequired()
                   .HasMaxLength(1000);

            builder.HasOne(x => x.User)
                   .WithMany(x => x.Comments)
                   .HasForeignKey(x => x.UserId);

            builder.HasOne(x => x.Post)
                   .WithMany(x => x.Comments)
                   .HasForeignKey(x => x.PostId)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(x => x.Product)
                   .WithMany(x => x.Comments)
                   .HasForeignKey(x => x.ProductId)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.Property(x => x.CreatedAt)
                   .IsRequired();

            builder.Property(x => x.IsActive)
                   .HasDefaultValue(true);
        }
    }
}
