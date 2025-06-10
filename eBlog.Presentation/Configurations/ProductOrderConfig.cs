using eBlog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eBlog.Persistence.Configurations
{
    public class ProductOrderConfig : IEntityTypeConfiguration<ProductOrder>
    {
        public void Configure(EntityTypeBuilder<ProductOrder> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Quantity)
                   .IsRequired();

            builder.Property(x => x.TotalPrice)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.Property(x => x.OrderedAt)
                   .IsRequired();

            builder.HasOne(x => x.Buyer)
                   .WithMany(u => u.ProductOrders)
                   .HasForeignKey(x => x.BuyerId);

            builder.HasOne(x => x.Product)
                   .WithMany(p => p.Orders)
                   .HasForeignKey(x => x.ProductId);
        }
    }
}
