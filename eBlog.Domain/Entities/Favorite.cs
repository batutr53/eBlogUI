using eBlog.Domain.Interfaces;

namespace eBlog.Domain.Entities
{
    public class Favorite : IEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid? PostId { get; set; }
        public Guid? CommentId { get; set; }
        public Guid? BookId { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation
        public User User { get; set; } = null!;
        public Post? Post { get; set; }
        public Comment? Comment { get; set; }
        public Product? Product { get; set; }
    }
}
