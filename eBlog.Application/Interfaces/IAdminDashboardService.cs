using eBlog.Application.DTOs.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBlog.Application.Interfaces
{
    public interface IAdminDashboardService
    {
        Task<List<TopLikedPostDto>> GetTopLikedPostsAsync();
        Task<DashboardTotalsDto> GetDashboardTotalsAsync();
        Task<List<TopSellingProductDto>> GetTopSellingProductsAsync();
        Task<List<TopCommentedPostDto>> GetTopCommentedPostsAsync();
        Task<List<TopBuyerDto>> GetTopBuyersAsync();
        Task<List<TopRatedProductDto>> GetTopRatedProductsAsync();
        Task<List<OrderStatusCountDto>> GetOrderStatusCountsAsync();
        Task<List<UserGrowthStatDto>> GetUserGrowthAsync(int days);
        Task<List<CategoryDistributionDto>> GetCategoryDistributionAsync();
        Task<List<ActiveAuthorDto>> GetActiveAuthorsAsync();
        Task<List<PostModuleUsageDto>> GetPostModuleUsageAsync();
        Task<List<CouponUsageDto>> GetCouponUsageAsync();
        Task<List<LoginActivityDto>> GetRecentLoginActivitiesAsync();
        Task<List<ErrorLogCountDto>> GetErrorLogCountsAsync(int days);
        Task<List<HourlyTrafficDto>> GetHourlyTrafficAsync();
        Task<List<TagUsageDto>> GetTagUsageStatsAsync();
        Task<PersonalSummaryDto> GetPersonalSummaryAsync(Guid userId);
    }

}
