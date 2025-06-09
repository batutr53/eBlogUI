using eBlog.Domain.Interfaces;

namespace eBlog.Domain.Entities
{

    public class User : IEntity, IAuditableEntity
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string? ProfileImageUrl { get; set; }
        public string? Bio { get; set; }

        // Çoklu dil için isim/biografi çeviri eklenebilir
        public bool IsAuthor { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        // Navigation
        public ICollection<Post> Posts { get; set; } = new List<Post>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<Product> Product { get; set; } = new List<Product>();
    }
}
