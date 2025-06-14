using eBlog.Domain.Models;
using eBlog.Domain.Models.Dashboard;


namespace eBlog.Domain.Interfaces.DAO
{
    public interface IAdminDashboardDao
    {
        Task<List<TopLikedPost>> GetTopLikedPostsAsync();
        Task<DashboardTotals> GetDashboardTotalsAsync();
        Task<List<TopSellingProduct>> GetTopSellingProductsAsync();
        Task<List<TopCommentedPost>> GetTopCommentedPostsAsync();
        Task<List<TopBuyer>> GetTopBuyersAsync();
        Task<List<TopRatedProduct>> GetTopRatedProductsAsync();
        Task<List<OrderStatusCount>> GetOrderStatusCountsAsync();
        Task<List<UserGrowthStat>> GetUserGrowthAsync(int days);
        Task<List<CategoryDistribution>> GetCategoryDistributionAsync();
        Task<List<ActiveAuthor>> GetActiveAuthorsAsync();
        Task<List<PostModuleUsage>> GetPostModuleUsageAsync();
        Task<List<CouponUsage>> GetCouponUsageAsync();
        Task<List<LoginActivity>> GetRecentLoginActivitiesAsync();
        Task<List<ErrorLogCount>> GetErrorLogCountsAsync(int days);
        Task<List<HourlyTraffic>> GetHourlyTrafficAsync();
        Task<List<TagUsage>> GetTagUsageStatsAsync();
        Task<PersonalSummary> GetPersonalSummaryAsync(Guid userId);
    }

}
