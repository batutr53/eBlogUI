using eBlog.Domain.Common;
using eBlog.Domain.Interfaces;

namespace eBlog.Domain.Entities
{
    public class Notification : BaseEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Message { get; set; } = null!;
        public string? Link { get; set; } // Bildirim tıklanınca gidilecek yer
        public bool IsRead { get; set; } = false;
        public DateTime CreatedAt { get; set; }

        // Navigation
        public User User { get; set; } = null!;
    }
}
