using Microsoft.AspNetCore.Mvc;
using eBlogUI.Business.Interfaces;
using eBlogUI.Models.Dashboard;
using Microsoft.Extensions.Logging;

namespace eBlogUI.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        private readonly IAdminDashboardApiService _dashboardService;
        private readonly ILogger<DashboardController> _logger;

        public DashboardController(IAdminDashboardApiService dashboardService, ILogger<DashboardController> logger)
        {
            _dashboardService = dashboardService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new DashboardViewModel();

            try
            {
                // Ana sayıları getir
                viewModel.Totals = await _dashboardService.GetDashboardTotalsAsync();
                
                // Paralel olarak diğer verileri getir
                var tasks = new List<Task>
                {
                    LoadTopLikedPosts(viewModel),
                    LoadTopSellingProducts(viewModel),
                    LoadTopCommentedPosts(viewModel),
                    LoadTopBuyers(viewModel),
                    LoadTopRatedProducts(viewModel),
                    LoadOrderStatusCounts(viewModel),
                    LoadUserGrowthStats(viewModel),
                    LoadCategoryDistribution(viewModel),
                    LoadActiveAuthors(viewModel),
                    LoadPostModuleUsage(viewModel),
                    LoadCouponUsage(viewModel),
                    LoadRecentLogins(viewModel),
                    LoadErrorLogCounts(viewModel),
                    LoadHourlyTraffic(viewModel),
                    LoadTagUsage(viewModel)
                };

                await Task.WhenAll(tasks);
            }
            catch (Exception ex)
            {
                // Log exception
                _logger.LogError(ex, "Dashboard verileri yüklenirken hata oluştu");
                TempData["ErrorMessage"] = "Dashboard verileri yüklenirken bir hata oluştu.";
            }

            return View(viewModel);
        }

        private async Task LoadTopLikedPosts(DashboardViewModel viewModel)
        {
            viewModel.TopLikedPosts = await _dashboardService.GetTopLikedPostsAsync() ?? new List<TopLikedPostViewModel>();
        }

        private async Task LoadTopSellingProducts(DashboardViewModel viewModel)
        {
            viewModel.TopSellingProducts = await _dashboardService.GetTopSellingProductsAsync() ?? new List<TopSellingProductViewModel>();
        }

        private async Task LoadTopCommentedPosts(DashboardViewModel viewModel)
        {
            viewModel.TopCommentedPosts = await _dashboardService.GetTopCommentedPostsAsync() ?? new List<TopCommentedPostViewModel>();
        }

        private async Task LoadTopBuyers(DashboardViewModel viewModel)
        {
            viewModel.TopBuyers = await _dashboardService.GetTopBuyersAsync() ?? new List<TopBuyerViewModel>();
        }

        private async Task LoadTopRatedProducts(DashboardViewModel viewModel)
        {
            viewModel.TopRatedProducts = await _dashboardService.GetTopRatedProductsAsync() ?? new List<TopRatedProductViewModel>();
        }

        private async Task LoadOrderStatusCounts(DashboardViewModel viewModel)
        {
            viewModel.OrderStatusCounts = await _dashboardService.GetOrderStatusCountsAsync() ?? new List<OrderStatusCountViewModel>();
        }

        private async Task LoadUserGrowthStats(DashboardViewModel viewModel)
        {
            viewModel.UserGrowthStats = await _dashboardService.GetUserGrowthStatsAsync(30) ?? new List<UserGrowthStatViewModel>();
        }

        private async Task LoadCategoryDistribution(DashboardViewModel viewModel)
        {
            viewModel.CategoryDistribution = await _dashboardService.GetCategoryDistributionAsync() ?? new List<CategoryDistributionViewModel>();
        }

        private async Task LoadActiveAuthors(DashboardViewModel viewModel)
        {
            viewModel.ActiveAuthors = await _dashboardService.GetActiveAuthorsAsync() ?? new List<ActiveAuthorViewModel>();
        }

        private async Task LoadPostModuleUsage(DashboardViewModel viewModel)
        {
            viewModel.PostModuleUsage = await _dashboardService.GetPostModuleUsageAsync() ?? new List<PostModuleUsageViewModel>();
        }

        private async Task LoadCouponUsage(DashboardViewModel viewModel)
        {
            viewModel.CouponUsage = await _dashboardService.GetCouponUsageAsync() ?? new List<CouponUsageViewModel>();
        }

        private async Task LoadRecentLogins(DashboardViewModel viewModel)
        {
            viewModel.RecentLogins = await _dashboardService.GetRecentLoginsAsync() ?? new List<LoginActivityViewModel>();
        }

        private async Task LoadErrorLogCounts(DashboardViewModel viewModel)
        {
            viewModel.ErrorLogCounts = await _dashboardService.GetErrorLogCountsAsync(7) ?? new List<ErrorLogCountViewModel>();
        }

        private async Task LoadHourlyTraffic(DashboardViewModel viewModel)
        {
            viewModel.HourlyTraffic = await _dashboardService.GetHourlyTrafficAsync() ?? new List<HourlyTrafficViewModel>();
        }

        private async Task LoadTagUsage(DashboardViewModel viewModel)
        {
            viewModel.TagUsage = await _dashboardService.GetTagUsageAsync() ?? new List<TagUsageViewModel>();
        }

        [HttpGet]
        public async Task<IActionResult> GetChartData(string chartType)
        {
            try
            {
                switch (chartType.ToLower())
                {
                    case "usergrowth":
                        var userGrowth = await _dashboardService.GetUserGrowthStatsAsync(30);
                        return Json(userGrowth);
                    
                    case "categorydistribution":
                        var categoryDistribution = await _dashboardService.GetCategoryDistributionAsync();
                        return Json(categoryDistribution);
                    
                    case "orderstatuses":
                        var orderStatuses = await _dashboardService.GetOrderStatusCountsAsync();
                        return Json(orderStatuses);
                    
                    case "hourlytraffic":
                        var hourlyTraffic = await _dashboardService.GetHourlyTrafficAsync();
                        return Json(hourlyTraffic);
                    
                    case "errorlogs":
                        var errorLogs = await _dashboardService.GetErrorLogCountsAsync(7);
                        return Json(errorLogs);
                    
                    default:
                        return BadRequest("Geçersiz grafik türü");
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Veri yüklenirken hata oluştu");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetUserGrowthData(int days = 30)
        {
            var data = await _dashboardService.GetUserGrowthStatsAsync(days);
            return Json(data ?? new List<UserGrowthStatViewModel>());
        }
    }
}
