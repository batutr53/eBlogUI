using eBlog.Domain.Entities;
using eBlog.Domain.Interfaces;
using eBlog.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace eBlog.Persistence.Repositories
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(AppDbContext context) : base(context) { }

        public async Task<Role?> GetByNameAsync(string name)
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.Name == name);
        }
    }
}
