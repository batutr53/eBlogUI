using eBlog.Domain.Common;
using eBlog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eBlog.Persistence.Configurations
{
    public class ProductOrderConfig : IEntityTypeConfiguration<ProductOrder>
    {
        public void Configure(EntityTypeBuilder<ProductOrder> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(o => o.Quantity)
                   .IsRequired();

            builder.Property(o => o.UnitPrice)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.Property(o => o.TotalPrice)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.Property(o => o.OrderDate)
                   .IsRequired();

            builder.Property(o => o.OrderedAt)
                   .HasDefaultValueSql("NOW()");

            builder.Property(o => o.Status)
                   .IsRequired()
                   .HasMaxLength(20);

            builder.HasOne(o => o.Buyer)
                   .WithMany(u => u.ProductOrders)
                   .HasForeignKey(o => o.BuyerId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(o => o.Product)
                   .WithMany(p => p.ProductOrders)
                   .HasForeignKey(o => o.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
