using eBlogUI.Business.Interfaces;
using eBlogUI.Models.Dashboard;
using System.Text.Json;

namespace eBlogUI.Business.Services
{
    public class AdminDashboardApiManager : IAdminDashboardApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly string _baseUrl;

        public AdminDashboardApiManager(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _baseUrl = _configuration["ApiSettings:BaseUrl"] ?? "https://localhost:7290";
        }

        public async Task<DashboardTotalsViewModel?> GetDashboardTotalsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/admin/dashboard/totals");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<DashboardTotalsViewModel>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                }
            }
            catch (Exception)
            {
                // Log exception
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
                    return JsonSerializer.Deserialize<List<TopLikedPostViewModel>>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                }
            }
            catch (Exception)
            {
                // Log exception
            }
            return new List<TopLikedPostViewModel>();
        }

        public async Task<List<TopSellingProductViewModel>?> GetTopSellingProductsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/admin/dashboard/top-selling-products");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<List<TopSellingProductViewModel>>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                }
            }
            catch (Exception)
            {
                // Log exception
            }
            return new List<TopSellingProductViewModel>();
        }

        public async Task<List<TopCommentedPostViewModel>?> GetTopCommentedPostsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/admin/dashboard/top-commented-posts");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<List<TopCommentedPostViewModel>>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                }
            }
            catch (Exception)
            {
                // Log exception
            }
            return new List<TopCommentedPostViewModel>();
        }

        public async Task<List<TopBuyerViewModel>?> GetTopBuyersAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/admin/dashboard/top-buyers");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<List<TopBuyerViewModel>>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                }
            }
            catch (Exception)
            {
                // Log exception
            }
            return new List<TopBuyerViewModel>();
        }

        public async Task<List<TopRatedProductViewModel>?> GetTopRatedProductsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/admin/dashboard/top-rated-products");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<List<TopRatedProductViewModel>>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                }
            }
            catch (Exception)
            {
                // Log exception
            }
            return new List<TopRatedProductViewModel>();
        }

        public async Task<List<OrderStatusCountViewModel>?> GetOrderStatusCountsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/admin/dashboard/order-status-counts");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<List<OrderStatusCountViewModel>>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                }
            }
            catch (Exception)
            {
                // Log exception
            }
            return new List<OrderStatusCountViewModel>();
        }

        public async Task<List<UserGrowthStatViewModel>?> GetUserGrowthAsync(int days = 30)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/admin/dashboard/user-growth?days={days}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<List<UserGrowthStatViewModel>>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                }
            }
            catch (Exception)
            {
                // Log exception
            }
            return new List<UserGrowthStatViewModel>();
        }

        public async Task<List<CategoryDistributionViewModel>?> GetCategoryDistributionAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/admin/dashboard/category-distribution");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<List<CategoryDistributionViewModel>>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                }
            }
            catch (Exception)
            {
                // Log exception
            }
            return new List<CategoryDistributionViewModel>();
        }

        public async Task<List<ActiveAuthorViewModel>?> GetActiveAuthorsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/admin/dashboard/active-authors");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<List<ActiveAuthorViewModel>>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                }
            }
            catch (Exception)
            {
                // Log exception
            }
            return new List<ActiveAuthorViewModel>();
        }

        public async Task<List<PostModuleUsageViewModel>?> GetPostModuleUsageAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/admin/dashboard/post-module-usage");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<List<PostModuleUsageViewModel>>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                }
            }
            catch (Exception)
            {
                // Log exception
            }
            return new List<PostModuleUsageViewModel>();
        }

        public async Task<List<CouponUsageViewModel>?> GetCouponUsageAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/admin/dashboard/coupon-usage");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<List<CouponUsageViewModel>>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                }
            }
            catch (Exception)
            {
                // Log exception
            }
            return new List<CouponUsageViewModel>();
        }

        public async Task<List<LoginActivityViewModel>?> GetRecentLoginActivitiesAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/admin/dashboard/recent-logins");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<List<LoginActivityViewModel>>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                }
            }
            catch (Exception)
            {
                // Log exception
            }
            return new List<LoginActivityViewModel>();
        }

        public async Task<List<ErrorLogCountViewModel>?> GetErrorLogCountsAsync(int days = 7)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/admin/dashboard/error-logs?days={days}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<List<ErrorLogCountViewModel>>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                }
            }
            catch (Exception)
            {
                // Log exception
            }
            return new List<ErrorLogCountViewModel>();
        }

        public async Task<List<HourlyTrafficViewModel>?> GetHourlyTrafficAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/admin/dashboard/hourly-traffic");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<List<HourlyTrafficViewModel>>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                }
            }
            catch (Exception)
            {
                // Log exception
            }
            return new List<HourlyTrafficViewModel>();
        }

        public async Task<List<TagUsageViewModel>?> GetTagUsageStatsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/admin/dashboard/tag-usage");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<List<TagUsageViewModel>>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                }
            }
            catch (Exception)
            {
                // Log exception
            }
            return new List<TagUsageViewModel>();
        }

        public async Task<PersonalSummaryViewModel?> GetPersonalSummaryAsync(Guid userId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/admin/dashboard/personal-summary/{userId}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<PersonalSummaryViewModel>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                }
            }
            catch (Exception)
            {
                // Log exception
            }
            return null;
        }
    }
}
