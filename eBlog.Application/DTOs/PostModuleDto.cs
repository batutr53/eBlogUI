using eBlog.Domain.Enums;

namespace eBlog.Application.DTOs
{
    public class PostModuleDto
    {
        public Guid? Id { get; set; }
        public ModuleType Type { get; set; }
        public string? Content { get; set; }
        public string? MediaUrl { get; set; }
        public int Order { get; set; }
    }

}
