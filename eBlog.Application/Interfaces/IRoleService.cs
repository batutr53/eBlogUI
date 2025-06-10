using eBlog.Application.DTOs;
using eBlog.Shared.Results;

namespace eBlog.Application.Interfaces
{
    public interface IRoleService
    {
        Task<List<RoleListDto>> GetAllAsync();
        Task<RoleDto?> GetByIdAsync(Guid id);
        Task<RoleDto?> GetByNameAsync(string name);
        Task<IResult> AddAsync(RoleCreateDto dto);
        Task<IDataResult<RoleDto>> FindOrCreateRoleByNameAsync(string roleName);

    }
}
