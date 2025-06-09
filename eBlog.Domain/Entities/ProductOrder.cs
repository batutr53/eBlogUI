using eBlog.Domain.Interfaces;

namespace eBlog.Domain.Entities
{
    public class ProductOrder : IEntity
    {
        public Guid Id { get; set; }
        public Guid BuyerId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderedAt { get; set; }
        public string Status { get; set; } = "pending"; // pending, paid, cancelled, shipped, completed

        // Navigation
        public User Buyer { get; set; } = null!;
        public Product Product { get; set; } = null!;
    }
}
