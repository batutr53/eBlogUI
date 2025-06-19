namespace eBlogUI.Models.Dtos.Tag
{
    public class TagListDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public int PostCount { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
