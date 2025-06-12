namespace eBlog.Application.DTOs.Dashboard
{
    public class TopBuyerDto
    {
        public Guid UserId { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int TotalOrders { get; set; }
        public decimal TotalSpent { get; set; }
    }
}
