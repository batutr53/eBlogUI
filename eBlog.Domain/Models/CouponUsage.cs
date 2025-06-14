namespace eBlog.Domain.Models
{
    public class CouponUsage
    {
        public string CouponCode { get; set; } = null!;
        public int UsageCount { get; set; }
    }
}
