namespace eBlog.Application.DTOs
{
    public class LikeDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid? PostId { get; set; }
        public Guid? ProductId { get; set; }
        public Guid? CommentId { get; set; }
    }

}
