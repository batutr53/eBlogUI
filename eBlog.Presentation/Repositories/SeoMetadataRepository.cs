using eBlog.Domain.Entities;
using eBlog.Domain.Interfaces;
using eBlog.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace eBlog.Persistence.Repositories
{
    public class SeoMetadataRepository : GenericRepository<SeoMetadata>, ISeoMetadataRepository
    {
        private readonly DbContext _context;

        public SeoMetadataRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<SeoMetadata>> GetVariantsByCanonicalId(Guid canonicalGroupId)
        {
            return await _context.Set<SeoMetadata>()
                .Where(x => x.CanonicalGroupId == canonicalGroupId)
                .ToListAsync();
        }
    }
}
