namespace eBlog.Application.DTOs
{
    public class CommentListDto
    {
        public Guid Id { get; set; }
        public string Content { get; set; } = null!;
        public string UserName { get; set; } = null!;
    }

    public class CommentCreateDto
    {
        public string Content { get; set; } = null!;
        public Guid? PostId { get; set; }
        public Guid? ProductId { get; set; }
        public Guid? ParentCommentId { get; set; }
    }

}
