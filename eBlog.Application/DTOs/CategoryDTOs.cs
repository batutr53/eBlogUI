namespace eBlog.Application.DTOs
{
    public class CategoryListDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
    }

    public class CategoryDetailDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string Slug { get; set; } = null!;
    }

    public class CategoryCreateDto
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }

    public class CategoryUpdateDto
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }

}
