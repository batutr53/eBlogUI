using eBlog.Application.DTOs;
using eBlog.Domain.Entities;
using eBlog.Shared.Results;

namespace eBlog.Application.Interfaces
{
    public interface IRoleService : IGenericService<Role, RoleDto, RoleCreateDto, RoleUpdateDto>
    {
        Task<RoleDto?> GetByNameAsync(string name);
        Task<IDataResult<RoleDto>> FindOrCreateRoleByNameAsync(string roleName);
    }
}
