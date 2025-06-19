using eBlogUI.Models.Dashboard;
using eBlog.Shared.Results;

namespace eBlogUI.Business.Interfaces
{
    public interface IAdminDashboardApiService
    {
        // Dashboard ana verileri
        Task<DashboardTotalsViewModel?> GetDashboardTotalsAsync();
        
        // En çok beğenilen postlar
        Task<List<TopLikedPostViewModel>?> GetTopLikedPostsAsync();
        
        // En çok satan ürünler
        Task<List<TopSellingProductViewModel>?> GetTopSellingProductsAsync();
        
        // En çok yorumlanan postlar  
        Task<List<TopCommentedPostViewModel>?> GetTopCommentedPostsAsync();
        
        // En çok alışveriş yapan kullanıcılar
        Task<List<TopBuyerViewModel>?> GetTopBuyersAsync();
        
        // En yüksek puanlı ürünler
        Task<List<TopRatedProductViewModel>?> GetTopRatedProductsAsync();
        
        // Sipariş durum sayıları
        Task<List<OrderStatusCountViewModel>?> GetOrderStatusCountsAsync();
        
        // Kullanıcı büyüme istatistikleri
        Task<List<UserGrowthStatViewModel>?> GetUserGrowthStatsAsync(int days = 30);
        
        // Kategori dağılımı
        Task<List<CategoryDistributionViewModel>?> GetCategoryDistributionAsync();
        
        // Aktif yazarlar
        Task<List<ActiveAuthorViewModel>?> GetActiveAuthorsAsync();
        
        // Post modül kullanımı
        Task<List<PostModuleUsageViewModel>?> GetPostModuleUsageAsync();
        
        // Kupon kullanımı
        Task<List<CouponUsageViewModel>?> GetCouponUsageAsync();
        
        // Son giriş aktiviteleri
        Task<List<LoginActivityViewModel>?> GetRecentLoginsAsync();
        
        // Hata log sayıları
        Task<List<ErrorLogCountViewModel>?> GetErrorLogCountsAsync(int days = 7);
        
        // Saatlik trafik
        Task<List<HourlyTrafficViewModel>?> GetHourlyTrafficAsync();
        
        // Tag kullanım istatistikleri
        Task<List<TagUsageViewModel>?> GetTagUsageAsync();
        
        // Kişisel özet
        Task<PersonalSummaryViewModel?> GetPersonalSummaryAsync(string userId);
    }
}
