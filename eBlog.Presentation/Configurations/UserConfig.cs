using eBlog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eBlog.Persistence.Configurations
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.UserName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(u => u.PasswordHash)
                .IsRequired();

            builder.HasMany(x => x.UserRoles)
                    .WithOne(x => x.User)
                    .HasForeignKey(x => x.UserId);

            builder.HasMany(x => x.Posts)
                   .WithOne(x => x.User)
                   .HasForeignKey(x => x.UserId);

        }
    }
}
