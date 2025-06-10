using eBlog.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBlog.Persistence.Configurations
{
    public class PostModuleConfig : IEntityTypeConfiguration<PostModule>
    {
        public void Configure(EntityTypeBuilder<PostModule> builder)
        {
            builder.HasOne(p => p.Post)
                   .WithMany(p => p.PostModules)
                   .HasForeignKey(p => p.PostId);

            builder.Property(p => p.Type).IsRequired();
            builder.Property(p => p.Order).IsRequired();
        }
    }
}
