namespace eBlog.Domain.Models.Dashboard
{
    public class DashboardTotals
    {
        public int UserCount { get; set; }
        public int PostCount { get; set; }
        public int CommentCount { get; set; }
        public int ProductCount { get; set; }
        public int OrderCount { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}
