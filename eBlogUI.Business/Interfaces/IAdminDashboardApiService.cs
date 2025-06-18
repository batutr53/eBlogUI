using eBlogUI.Models.Dashboard;

namespace eBlogUI.Business.Interfaces
{
    public interface IAdminDashboardApiService
    {
        Task<DashboardTotalsViewModel?> GetDashboardTotalsAsync();
        Task<List<TopLikedPostViewModel>?> GetTopLikedPostsAsync();
        Task<List<TopSellingProductViewModel>?> GetTopSellingProductsAsync();
        Task<List<TopCommentedPostViewModel>?> GetTopCommentedPostsAsync();
        Task<List<TopBuyerViewModel>?> GetTopBuyersAsync();
        Task<List<TopRatedProductViewModel>?> GetTopRatedProductsAsync();
        Task<List<OrderStatusCountViewModel>?> GetOrderStatusCountsAsync();
        Task<List<UserGrowthStatViewModel>?> GetUserGrowthAsync(int days = 30);
        Task<List<CategoryDistributionViewModel>?> GetCategoryDistributionAsync();
        Task<List<ActiveAuthorViewModel>?> GetActiveAuthorsAsync();
        Task<List<PostModuleUsageViewModel>?> GetPostModuleUsageAsync();
        Task<List<CouponUsageViewModel>?> GetCouponUsageAsync();
        Task<List<LoginActivityViewModel>?> GetRecentLoginActivitiesAsync();
        Task<List<ErrorLogCountViewModel>?> GetErrorLogCountsAsync(int days = 7);
        Task<List<HourlyTrafficViewModel>?> GetHourlyTrafficAsync();
        Task<List<TagUsageViewModel>?> GetTagUsageStatsAsync();
        Task<PersonalSummaryViewModel?> GetPersonalSummaryAsync(Guid userId);
    }
}
