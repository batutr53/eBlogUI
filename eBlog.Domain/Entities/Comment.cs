using eBlog.Domain.Common;
using eBlog.Domain.Interfaces;

namespace eBlog.Domain.Entities
{
    public class Comment : BaseEntity
    {
        public Guid Id { get; set; }
        public string Content { get; set; } = null!;
        public Guid UserId { get; set; }
        public Guid? PostId { get; set; }
        public Guid? BookId { get; set; }
        public Guid? ParentCommentId { get; set; } // Cevaplar için

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        // Navigation
        public User User { get; set; } = null!;
        public Post? Post { get; set; }
        public Product? Product { get; set; }
        public Comment? ParentComment { get; set; }
        public ICollection<Comment> Replies { get; set; } = new List<Comment>();
        public ICollection<Like> Likes { get; set; } = new List<Like>();
        public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
    }
}
