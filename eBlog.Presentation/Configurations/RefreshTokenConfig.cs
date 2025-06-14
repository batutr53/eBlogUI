using eBlog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eBlog.Infrastructure.Persistence.Configurations
{
    public class RefreshTokenConfig : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Token).IsRequired();
            builder.Property(x => x.Expires).IsRequired();
            builder.Property(x => x.Created).IsRequired();

            builder.HasOne(x => x.User)
                   .WithMany(x => x.RefreshTokens)
                   .HasForeignKey(x => x.UserId);
        }
    }
}
