using eBlog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eBlog.Persistence.Configurations
{
    public class FavoriteConfig : IEntityTypeConfiguration<Favorite>
    {
        public void Configure(EntityTypeBuilder<Favorite> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CreatedAt)
                   .IsRequired();

            builder.HasOne(x => x.User)
                   .WithMany(u => u.Favorites)
                   .HasForeignKey(x => x.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Post)
                   .WithMany(p => p.Favorites)
                   .HasForeignKey(x => x.PostId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Product)
                   .WithMany(p => p.Favorites)
                   .HasForeignKey(x => x.ProductId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Comment)
                   .WithMany(c => c.Favorites)
                   .HasForeignKey(x => x.CommentId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
