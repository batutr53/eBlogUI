using eBlog.Application.DTOs;

namespace eBlog.Application.Interfaces
{
    public interface IPostModuleService
    {
        Task<List<PostModuleDto>> GetModulesByPostIdAsync(Guid postId);
        Task UpdatePostModulesAsync(Guid postId, List<PostModuleDto> modules);
    }
}
