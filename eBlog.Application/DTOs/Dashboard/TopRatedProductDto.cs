namespace eBlog.Application.DTOs.Dashboard
{
    public class TopRatedProductDto
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public double AverageRating { get; set; }
        public int TotalReviews { get; set; }
    }
}
