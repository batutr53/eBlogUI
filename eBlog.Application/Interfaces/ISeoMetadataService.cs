using eBlog.Application.DTOs;
using eBlog.Domain.Entities;

namespace eBlog.Application.Interfaces
{
    public interface ISeoMetadataService : IGenericService<SeoMetadata, SeoMetadataDto, SeoMetadataCreateDto, SeoMetadataUpdateDto>
    {
        Task<List<SeoMetadataDto>> GetVariantsByPostIdAsync(Guid postId);
    }

}
