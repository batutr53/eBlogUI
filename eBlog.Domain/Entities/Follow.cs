using eBlog.Domain.Interfaces;

namespace eBlog.Domain.Entities
{

    public class Follow : IEntity
    {
        public Guid Id { get; set; }
        public Guid FollowerId { get; set; }    // Takip eden
        public Guid FollowingId { get; set; }   // Takip edilen
        public DateTime CreatedAt { get; set; }

        // Navigation
        public User Follower { get; set; } = null!;
        public User Following { get; set; } = null!;
    }
}
