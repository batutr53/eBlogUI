namespace eBlog.Application.DTOs.Dashboard
{
    public class CouponUsageDto
    {
        public string CouponCode { get; set; } = null!;
        public int UsageCount { get; set; }
    }
}
