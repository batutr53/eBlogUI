using eBlog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eBlog.Persistence.Configurations
{
    public class FollowConfig : IEntityTypeConfiguration<Follow>
    {
        public void Configure(EntityTypeBuilder<Follow> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.FollowedAt)
                   .IsRequired();

            builder.HasOne(x => x.Follower)
                   .WithMany(u => u.Following)
                   .HasForeignKey(x => x.FollowerId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Following)
                   .WithMany(u => u.Followers)
                   .HasForeignKey(x => x.FollowingId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasIndex(x => new { x.FollowerId, x.FollowingId }).IsUnique();
        }
    }
}
