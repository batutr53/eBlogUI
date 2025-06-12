namespace eBlog.Domain.Models.Dashboard
{
    public class TopSellingProduct
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public int TotalQuantitySold { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}
