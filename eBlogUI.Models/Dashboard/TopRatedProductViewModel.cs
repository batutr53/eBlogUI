namespace eBlogUI.Models.Dashboard
{
    public class TopRatedProductViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal AverageRating { get; set; }
        public int ReviewCount { get; set; }
        public string? ImageUrl { get; set; }
    }
}
