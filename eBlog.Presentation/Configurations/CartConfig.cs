using eBlog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eBlog.Persistence.Configurations
{
    public class CartConfig : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.User)
                   .WithOne(x => x.Cart)
                   .HasForeignKey<Cart>(x => x.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.CartItems)
                   .WithOne(x => x.Cart)
                   .HasForeignKey(x => x.CartId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.UserId).IsRequired();
            builder.ToTable("Carts");
        }
    }
}
