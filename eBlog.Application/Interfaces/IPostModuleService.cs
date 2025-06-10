using eBlog.Application.DTOs;
using eBlog.Domain.Entities;

namespace eBlog.Application.Interfaces
{
    public interface IPostModuleService : IGenericService<PostModule,PostModuleListDto, PostModuleCreateDto, PostModuleUpdateDto>
    {
        Task<List<PostModuleDto>> GetModulesByPostIdAsync(Guid postId);
        Task UpdatePostModulesAsync(Guid postId, List<PostModuleDto> modules);
    }
}
