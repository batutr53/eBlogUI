namespace eBlog.Application.DTOs
{
    public class PostWithModulesDto
    {
        public Guid? Id { get; set; }
        public string Title { get; set; } = null!;
        public List<PostModuleDto> Modules { get; set; } = new();
    }
}
