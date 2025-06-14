namespace eBlog.Application.DTOs
{
    public class TagListDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
    }

    public class TagCreateDto
    {
        public string Name { get; set; } = null!;
    }
    public class TagDetailDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Slug { get; set; } = null!;
        public string? LanguageCode { get; set; }
        // İstersen ekstra bilgiler ekleyebilirsin (örn. tag’a ait kaç post var?)
        public int PostCount { get; set; }
    }

}
