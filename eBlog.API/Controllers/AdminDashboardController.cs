using eBlog.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eBlog.API.Controllers
{
    [ApiController]
    [Route("api/admin/dashboard")]
    [Authorize(Roles = "Admin")]
    public class AdminDashboardController : ControllerBase
    {
        private readonly IAdminDashboardService _dashboardService;

        public AdminDashboardController(IAdminDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }


        [HttpGet("totals")]
        public async Task<IActionResult> GetDashboardTotals()
            => Ok(await _dashboardService.GetDashboardTotalsAsync());

        [HttpGet("top-liked-posts")]
        public async Task<IActionResult> GetTopLikedPosts()
            => Ok(await _dashboardService.GetTopLikedPostsAsync());

        [HttpGet("top-selling-products")]
        public async Task<IActionResult> GetTopSellingProducts()
            => Ok(await _dashboardService.GetTopSellingProductsAsync());

        [HttpGet("top-commented-posts")]
        public async Task<IActionResult> GetTopCommentedPosts()
            => Ok(await _dashboardService.GetTopCommentedPostsAsync());

        [HttpGet("top-buyers")]
        public async Task<IActionResult> GetTopBuyers()
            => Ok(await _dashboardService.GetTopBuyersAsync());

        [HttpGet("top-rated-products")]
        public async Task<IActionResult> GetTopRatedProducts()
            => Ok(await _dashboardService.GetTopRatedProductsAsync());

        [HttpGet("order-status-counts")]
        public async Task<IActionResult> GetOrderStatusCounts()
            => Ok(await _dashboardService.GetOrderStatusCountsAsync());

        [HttpGet("user-growth")]
        public async Task<IActionResult> GetUserGrowth([FromQuery] int days = 30)
            => Ok(await _dashboardService.GetUserGrowthAsync(days));

        [HttpGet("category-distribution")]
        public async Task<IActionResult> GetCategoryDistribution()
            => Ok(await _dashboardService.GetCategoryDistributionAsync());

        [HttpGet("active-authors")]
        public async Task<IActionResult> GetActiveAuthors()
            => Ok(await _dashboardService.GetActiveAuthorsAsync());

        [HttpGet("post-module-usage")]
        public async Task<IActionResult> GetPostModuleUsage()
            => Ok(await _dashboardService.GetPostModuleUsageAsync());

        [HttpGet("coupon-usage")]
        public async Task<IActionResult> GetCouponUsage()
            => Ok(await _dashboardService.GetCouponUsageAsync());

        [HttpGet("recent-logins")]
        public async Task<IActionResult> GetRecentLoginActivities()
            => Ok(await _dashboardService.GetRecentLoginActivitiesAsync());

        [HttpGet("error-logs")]
        public async Task<IActionResult> GetErrorLogCounts([FromQuery] int days = 7)
            => Ok(await _dashboardService.GetErrorLogCountsAsync(days));

        [HttpGet("hourly-traffic")]
        public async Task<IActionResult> GetHourlyTraffic()
            => Ok(await _dashboardService.GetHourlyTrafficAsync());

        [HttpGet("tag-usage")]
        public async Task<IActionResult> GetTagUsageStats()
            => Ok(await _dashboardService.GetTagUsageStatsAsync());

        [HttpGet("personal-summary/{userId}")]
        public async Task<IActionResult> GetPersonalSummary(Guid userId)
            => Ok(await _dashboardService.GetPersonalSummaryAsync(userId));
    }

}
