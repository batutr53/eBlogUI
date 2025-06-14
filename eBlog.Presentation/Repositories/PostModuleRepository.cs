using eBlog.Domain.Entities;
using eBlog.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using eBlog.Persistence.Contexts;

namespace eBlog.Persistence.Repositories
{
    public class PostModuleRepository : GenericRepository<PostModule>, IPostModuleRepository
    {
        private readonly AppDbContext _context;

        public PostModuleRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<PostModule>> GetModulesByPostIdAsync(Guid postId)
        {
            return await _context.PostModules
                .Where(pm => pm.PostId == postId)
                .OrderBy(pm => pm.Order)
                .ToListAsync();
        }

        public async Task DeleteModulesByPostIdAsync(Guid postId)
        {
            var existingModules = await _context.PostModules
                .Where(pm => pm.PostId == postId)
                .ToListAsync();

            _context.PostModules.RemoveRange(existingModules);
        }

        public async Task AddRangeAsync(IEnumerable<PostModule> modules)
        {
            await _context.PostModules.AddRangeAsync(modules);
        }
    }
}
