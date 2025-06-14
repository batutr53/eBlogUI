namespace eBlog.Domain.Models.Dashboard
{
    public class TopRatedProduct
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public double AverageRating { get; set; }
        public int TotalReviews { get; set; }
    }
}
