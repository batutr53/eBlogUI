using eBlog.Domain.Common;
using eBlog.Domain.Interfaces;

namespace eBlog.Domain.Entities
{

    public class Follow : BaseEntity
    {
        public Guid Id { get; set; }

        public Guid FollowerId { get; set; }
        public User Follower { get; set; }

        public Guid FollowingId { get; set; }
        public User Following { get; set; }

        public DateTime FollowedAt { get; set; }
    }
}
