using eBlog.Domain.Entities;

namespace eBlog.Domain.Interfaces
{
    public interface ISeoMetadataRepository : IGenericRepository<SeoMetadata>
    {
        Task<List<SeoMetadata>> GetVariantsByCanonicalId(Guid canonicalGroupId);
    }
}
