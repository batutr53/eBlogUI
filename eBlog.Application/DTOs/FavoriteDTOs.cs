namespace eBlog.Application.DTOs
{

    public class FavoriteListDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; } = null!;
        public Guid? PostId { get; set; }
        public string? PostTitle { get; set; }
        public Guid? ProductId { get; set; }
        public string? ProductName { get; set; }
        public Guid? CommentId { get; set; }
        public string? CommentContent { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    // Ekleme için
    public class FavoriteCreateDto
    {
        public Guid? PostId { get; set; }
        public Guid? ProductId { get; set; }
        public Guid? CommentId { get; set; }
    }
}
