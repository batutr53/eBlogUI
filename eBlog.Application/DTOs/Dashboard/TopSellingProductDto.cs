namespace eBlog.Application.DTOs.Dashboard
{
    public class TopSellingProductDto
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public int TotalQuantitySold { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}
