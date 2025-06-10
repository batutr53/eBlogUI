using eBlog.Domain.Common;
using eBlog.Domain.Interfaces;

namespace eBlog.Domain.Entities
{

    public class User : BaseEntity
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string? ProfileImageUrl { get; set; }
        public string? Bio { get; set; }
        public string? PasswordResetToken { get; set; }
        public DateTime? ResetTokenExpires { get; set; }

        public string? EmailVerificationToken { get; set; }
        public bool IsEmailVerified { get; set; } = false;
        // Çoklu dil için isim/biografi çeviri eklenebilir
        public bool IsAuthor { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        // Navigation
        public Cart Cart { get; set; }
        public ICollection<Notification> Notifications { get; set; }
        public ICollection<Favorite> Favorites { get; set; }
        public ICollection<Post> Posts { get; set; } = new List<Post>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<Product> Product { get; set; } = new List<Product>();
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
        public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
        public ICollection<Follow> Followers { get; set; }
        public ICollection<Follow> Following { get; set; }
        public ICollection<ProductOrder> Orders { get; set; }
        public ICollection<ProductOrder> ProductOrders { get; set; }



    }
}
