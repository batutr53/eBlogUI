using eBlog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eBlog.Persistence.Configurations
{
    public class TagConfig : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(x => x.Slug)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.HasIndex(x => x.Slug)
                   .IsUnique();

            builder.HasMany(x => x.PostModules)
                   .WithMany(x => x.Tags)
                   .UsingEntity(j => j.ToTable("PostModuleTags"));
        }
    }
}
