namespace eBlog.Application.DTOs
{
    public class PostModuleListDto
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string Content { get; set; }
        public int Order { get; set; }
    }

}
