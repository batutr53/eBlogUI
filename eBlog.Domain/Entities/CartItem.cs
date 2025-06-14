using eBlog.Domain.Interfaces;

namespace eBlog.Domain.Entities
{
    public class CartItem : IEntity
    {
        public Guid Id { get; set; }
        public Guid CartId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        // Navigation
        public Cart Cart { get; set; } = null!;
        public Product Product { get; set; } = null!;
    }
}
