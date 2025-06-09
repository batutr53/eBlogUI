namespace eBlog.Application.DTOs
{
    public class FollowListDto
    {
        public Guid Id { get; set; }
        public Guid FollowerId { get; set; }
        public string FollowerUserName { get; set; } = null!;
        public Guid FollowingId { get; set; }
        public string FollowingUserName { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }

    public class FollowCreateDto
    {
        public Guid FollowingId { get; set; }
    }
}
