using eBlog.Application.DTOs;
using eBlog.Domain.Entities;
using eBlog.Domain.Interfaces;
using eBlog.Shared.Results;

namespace eBlog.Application.Interfaces
{
    public interface IPostService : IGenericService<Post,PostListDto, PostCreateDto, PostUpdateDto>
    {
        Task<IDataResult<List<PostListDto>>> GetRecentPostsAsync(int count);

    }
}
