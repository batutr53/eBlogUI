using eBlog.Application.DTOs;
using eBlog.Domain.Entities;
using eBlog.Shared.Results;

namespace eBlog.Application.Interfaces
{
    public interface ISeoMetadataService : IGenericService<SeoMetadata, SeoMetadataDto, SeoMetadataCreateDto, SeoMetadataUpdateDto>
    {
        Task<List<SeoMetadataDto>> GetVariantsByPostIdAsync(Guid postId);
        Task<IDataResult<SeoMetadataDto>> AddVariantAsync(Guid canonicalSeoId, SeoMetadataCreateDto dto);
    }

}
