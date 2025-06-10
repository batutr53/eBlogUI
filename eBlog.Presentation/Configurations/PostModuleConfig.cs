using eBlog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eBlog.Persistence.Configurations
{
    public class PostModuleConfig : IEntityTypeConfiguration<PostModule>
    {
        public void Configure(EntityTypeBuilder<PostModule> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Type)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(x => x.Content)
                   .HasMaxLength(5000); // opsiyonel: content uzunluğu

            builder.Property(x => x.Order)
                   .IsRequired();

            builder.HasOne(x => x.Post)
                   .WithMany(p => p.PostModules)
                   .HasForeignKey(x => x.PostId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
