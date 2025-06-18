namespace eBlogUI.Models.Dashboard
{
    public class DashboardViewModel
    {
        public DashboardTotalsViewModel? Totals { get; set; }
        public List<TopLikedPostViewModel> TopLikedPosts { get; set; } = new();
        public List<TopSellingProductViewModel> TopSellingProducts { get; set; } = new();
        public List<TopCommentedPostViewModel> TopCommentedPosts { get; set; } = new();
        public List<TopBuyerViewModel> TopBuyers { get; set; } = new();
        public List<TopRatedProductViewModel> TopRatedProducts { get; set; } = new();
        public List<OrderStatusCountViewModel> OrderStatusCounts { get; set; } = new();
        public List<UserGrowthStatViewModel> UserGrowthStats { get; set; } = new();
        public List<CategoryDistributionViewModel> CategoryDistribution { get; set; } = new();
        public List<ActiveAuthorViewModel> ActiveAuthors { get; set; } = new();
        public List<PostModuleUsageViewModel> PostModuleUsage { get; set; } = new();
        public List<CouponUsageViewModel> CouponUsage { get; set; } = new();
        public List<LoginActivityViewModel> RecentLoginActivities { get; set; } = new();
        public List<ErrorLogCountViewModel> ErrorLogCounts { get; set; } = new();
        public List<HourlyTrafficViewModel> HourlyTraffic { get; set; } = new();
        public List<TagUsageViewModel> TagUsageStats { get; set; } = new();
    }
}
