using eBlog.Application.DTOs;
using eBlog.Domain.Entities;
using eBlog.Domain.Interfaces;
using eBlog.Shared.Results;

namespace eBlog.Application.Interfaces
{
    public interface IUserService : IGenericService<User,UserListDto, UserCreateDto, UserUpdateDto>
    {
        Task<IDataResult<UserDetailDto>> GetByEmailAsync(string email);
        Task<IDataResult<List<UserListDto>>> GetActiveAuthorsAsync(int count);

    }
}
