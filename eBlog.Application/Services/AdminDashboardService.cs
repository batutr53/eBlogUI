using AutoMapper;
using eBlog.Application.DTOs.Dashboard;
using eBlog.Application.Interfaces;
using eBlog.Domain.Interfaces.DAO;
using Microsoft.AspNetCore.Mvc;

namespace eBlog.Application.Services
{

    public class AdminDashboardService : IAdminDashboardService
    {
        private readonly IAdminDashboardDao _dashboardDao;
        private readonly IMapper _mapper;
        public AdminDashboardService(IAdminDashboardDao dashboardDao, IMapper mapper)
        {
            _dashboardDao = dashboardDao;
            _mapper = mapper;
        }

        public async Task<List<TopLikedPostDto>> GetTopLikedPostsAsync()
        {
            var result = await _dashboardDao.GetTopLikedPostsAsync();
            return _mapper.Map<List<TopLikedPostDto>>(result);
        }

        public async Task<DashboardTotalsDto> GetDashboardTotalsAsync()
        {
            var result = await _dashboardDao.GetDashboardTotalsAsync();
            return _mapper.Map<DashboardTotalsDto>(result);
        }

        public async Task<List<TopSellingProductDto>> GetTopSellingProductsAsync()
        {
            var result = await _dashboardDao.GetTopSellingProductsAsync();
            return _mapper.Map<List<TopSellingProductDto>>(result);
        }

        public async Task<List<TopCommentedPostDto>> GetTopCommentedPostsAsync()
        {
            var result = await _dashboardDao.GetTopCommentedPostsAsync();
            return _mapper.Map<List<TopCommentedPostDto>>(result);
        }

        public async Task<List<TopBuyerDto>> GetTopBuyersAsync()
        {
            var result = await _dashboardDao.GetTopBuyersAsync();
            return _mapper.Map<List<TopBuyerDto>>(result);
        }

        public async Task<List<TopRatedProductDto>> GetTopRatedProductsAsync()
        {
            var result = await _dashboardDao.GetTopRatedProductsAsync();
            return _mapper.Map<List<TopRatedProductDto>>(result);
        }

        public async Task<List<OrderStatusCountDto>> GetOrderStatusCountsAsync()
        {
            var result = await _dashboardDao.GetOrderStatusCountsAsync();
            return _mapper.Map<List<OrderStatusCountDto>>(result);
        }

        public async Task<List<UserGrowthStatDto>> GetUserGrowthAsync(int days)
        {
            var result = await _dashboardDao.GetUserGrowthAsync(days);
            return _mapper.Map<List<UserGrowthStatDto>>(result);
        }

        public async Task<List<CategoryDistributionDto>> GetCategoryDistributionAsync()
        {
            var result = await _dashboardDao.GetCategoryDistributionAsync();
            return _mapper.Map<List<CategoryDistributionDto>>(result);
        }

        public async Task<List<ActiveAuthorDto>> GetActiveAuthorsAsync()
        {
            var result = await _dashboardDao.GetActiveAuthorsAsync();
            return _mapper.Map<List<ActiveAuthorDto>>(result);
        }

        public async Task<List<PostModuleUsageDto>> GetPostModuleUsageAsync()
        {
            var result = await _dashboardDao.GetPostModuleUsageAsync();
            return _mapper.Map<List<PostModuleUsageDto>>(result);
        }

        public async Task<List<CouponUsageDto>> GetCouponUsageAsync()
        {
            var result = await _dashboardDao.GetCouponUsageAsync();
            return _mapper.Map<List<CouponUsageDto>>(result);
        }

        public async Task<List<LoginActivityDto>> GetRecentLoginActivitiesAsync()
        {
            var result = await _dashboardDao.GetRecentLoginActivitiesAsync();
            return _mapper.Map<List<LoginActivityDto>>(result);
        }

        public async Task<List<ErrorLogCountDto>> GetErrorLogCountsAsync(int days)
        {
            var result = await _dashboardDao.GetErrorLogCountsAsync(days);
            return _mapper.Map<List<ErrorLogCountDto>>(result);
        }

        public async Task<List<HourlyTrafficDto>> GetHourlyTrafficAsync()
        {
            var result = await _dashboardDao.GetHourlyTrafficAsync();
            return _mapper.Map<List<HourlyTrafficDto>>(result);
        }

        public async Task<List<TagUsageDto>> GetTagUsageStatsAsync()
        {
            var result = await _dashboardDao.GetTagUsageStatsAsync();
            return _mapper.Map<List<TagUsageDto>>(result);
        }

        public async Task<PersonalSummaryDto> GetPersonalSummaryAsync(Guid userId)
        {
            var result = await _dashboardDao.GetPersonalSummaryAsync(userId);
            return _mapper.Map<PersonalSummaryDto>(result);
        }
    }

}
