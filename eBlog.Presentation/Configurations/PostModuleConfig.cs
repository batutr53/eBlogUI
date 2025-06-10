using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using eBlog.Domain.Entities;

namespace eBlog.Persistence.Configurations
{
    public class PostModuleConfig : IEntityTypeConfiguration<PostModule>
    {
        public void Configure(EntityTypeBuilder<PostModule> builder)
        {
            builder.HasKey(pm => pm.Id);

            builder.Property(pm => pm.Type)
                   .IsRequired()
                   .HasMaxLength(50); // e.g. Text, Image, Quote, etc.

            builder.Property(pm => pm.Content)
                   .HasColumnType("text");

            builder.Property(pm => pm.Order)
                   .IsRequired();

            builder.Property(pm => pm.MediaUrl)
                   .HasMaxLength(500);

            // ✅ Post - One to Many
            builder.HasOne(pm => pm.Post)
                   .WithMany(p => p.Modules)
                   .HasForeignKey(pm => pm.PostId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
