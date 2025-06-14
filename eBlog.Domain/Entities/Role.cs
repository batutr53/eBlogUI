using eBlog.Domain.Interfaces;

namespace eBlog.Domain.Entities
{
    public class Role : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<UserRole> UserRoles { get; set; }
    }

}
