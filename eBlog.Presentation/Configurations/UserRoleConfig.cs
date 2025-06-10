using eBlog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eBlog.Persistence.Configurations
{
    public class UserRoleConfig : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasKey(ur => ur.Id);

            builder.Property(ur => ur.RoleName)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasIndex(ur => new { ur.UserId, ur.RoleName }).IsUnique(); // Bir kullanıcıda aynı rol bir kez bulunabilir
        }
    }
}
