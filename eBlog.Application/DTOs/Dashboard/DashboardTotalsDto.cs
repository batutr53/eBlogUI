namespace eBlog.Application.DTOs.Dashboard
{
    public class DashboardTotalsDto
    {
        public int UserCount { get; set; }
        public int PostCount { get; set; }
        public int CommentCount { get; set; }
        public int ProductCount { get; set; }
        public int OrderCount { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}
