namespace eBlog.Domain.Models
{
    public class LoginActivity
    {
        public string Email { get; set; } = null!;
        public string IpAddress { get; set; } = null!;
        public DateTime LoginTime { get; set; }
    }
}
