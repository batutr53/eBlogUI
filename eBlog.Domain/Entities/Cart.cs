using eBlog.Domain.Interfaces;

namespace eBlog.Domain.Entities
{
    public class Cart : IEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        // Navigation
        public User User { get; set; } = null!;
        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
    }
}
