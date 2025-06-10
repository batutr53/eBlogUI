using eBlog.Domain.Interfaces;

namespace eBlog.Domain.Entities
{
    public class RefreshToken : IEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public bool IsExpired => DateTime.UtcNow >= Expires;
        public DateTime Created { get; set; }
        public string CreatedByIp { get; set; }

        public User User { get; set; }
    }

}
