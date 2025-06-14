namespace eBlog.Application.DTOs
{
    public class PostModuleCreateDto
    {
        public Guid PostId { get; set; }
        public string Type { get; set; }
        public string Content { get; set; }
        public int Order { get; set; }
    }

}
