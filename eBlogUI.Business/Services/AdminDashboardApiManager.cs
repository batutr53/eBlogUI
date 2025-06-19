using eBlogUI.Business.Interfaces;
using eBlogUI.Models.Dashboard;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using eBlog.Shared.Results;

namespace eBlogUI.Business.Services
{
    public class AdminDashboardApiManager : IAdminDashboardApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly string _baseUrl;
        private readonly ILogger<AdminDashboardApiManager> _logger;

        public AdminDashboardApiManager(HttpClient httpClient, IConfiguration configuration, ILogger<AdminDashboardApiManager> logger)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _baseUrl = _configuration["ApiSettings:BaseUrl"] ?? "https://localhost:7001";
            _logger = logger;
        }

        public async Task<DashboardTotalsViewModel?> GetDashboardTotalsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/admin/dashboard/totals");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<DashboardTotalsViewModel>(json);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting dashboard totals");
            }
            return null;
        }

        public async Task<List<TopLikedPostViewModel>?> GetTopLikedPostsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/admin/dashboard/top-liked-posts");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<TopLikedPostViewModel>>(json);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting top liked posts");
            }
            return null;
        }

        public async Task<List<TopSellingProductViewModel>?> GetTopSellingProductsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/admin/dashboard/top-selling-products");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<TopSellingProductViewModel>>(json);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting top selling products");
            }
            return null;
        }

        public async Task<List<TopCommentedPostViewModel>?> GetTopCommentedPostsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/admin/dashboard/top-commented-posts");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<TopCommentedPostViewModel>>(json);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting top commented posts");
            }
            return null;
        }

        public async Task<List<TopBuyerViewModel>?> GetTopBuyersAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/admin/dashboard/top-buyers");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<TopBuyerViewModel>>(json);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting top buyers");
            }
            return null;
        }

        public async Task<List<TopRatedProductViewModel>?> GetTopRatedProductsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/admin/dashboard/top-rated-products");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<TopRatedProductViewModel>>(json);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting top rated products");
            }
            return null;
        }

        public async Task<List<OrderStatusCountViewModel>?> GetOrderStatusCountsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/admin/dashboard/order-status-counts");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<OrderStatusCountViewModel>>(json);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting order status counts");
            }
            return null;
        }

        public async Task<List<UserGrowthStatViewModel>?> GetUserGrowthStatsAsync(int days = 30)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/admin/dashboard/user-growth?days={days}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<UserGrowthStatViewModel>>(json);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting user growth stats");
            }
            return null;
        }

        public async Task<List<CategoryDistributionViewModel>?> GetCategoryDistributionAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/admin/dashboard/category-distribution");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<CategoryDistributionViewModel>>(json);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting category distribution");
            }
            return null;
        }

        public async Task<List<ActiveAuthorViewModel>?> GetActiveAuthorsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/admin/dashboard/active-authors");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<ActiveAuthorViewModel>>(json);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting active authors");
            }
            return null;
        }

        public async Task<List<PostModuleUsageViewModel>?> GetPostModuleUsageAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/admin/dashboard/post-module-usage");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<PostModuleUsageViewModel>>(json);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting post module usage");
            }
            return null;
        }

        public async Task<List<CouponUsageViewModel>?> GetCouponUsageAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/admin/dashboard/coupon-usage");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<CouponUsageViewModel>>(json);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting coupon usage");
            }
            return null;
        }

        public async Task<List<LoginActivityViewModel>?> GetRecentLoginsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/admin/dashboard/recent-logins");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<LoginActivityViewModel>>(json);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting recent logins");
            }
            return null;
        }

        public async Task<List<ErrorLogCountViewModel>?> GetErrorLogCountsAsync(int days = 7)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/admin/dashboard/error-logs?days={days}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<ErrorLogCountViewModel>>(json);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting error log counts");
            }
            return null;
        }

        public async Task<List<HourlyTrafficViewModel>?> GetHourlyTrafficAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/admin/dashboard/hourly-traffic");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<HourlyTrafficViewModel>>(json);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting hourly traffic");
            }
            return null;
        }

        public async Task<List<TagUsageViewModel>?> GetTagUsageAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/admin/dashboard/tag-usage");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<TagUsageViewModel>>(json);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting tag usage");
            }
            return null;
        }

        public async Task<PersonalSummaryViewModel?> GetPersonalSummaryAsync(string userId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/admin/dashboard/personal-summary/{userId}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<PersonalSummaryViewModel>(json);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting personal summary");
            }
            return null;
        }
    }
}
