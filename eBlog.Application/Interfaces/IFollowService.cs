using eBlog.Application.DTOs;
using eBlog.Domain.Entities;
using eBlog.Domain.Interfaces;
using eBlog.Shared.Results;

namespace eBlog.Application.Interfaces
{
   
        public interface IFollowService : IGenericService<Follow, FollowListDto, FollowCreateDto, FollowCreateDto>
        {
            Task<IDataResult<List<FollowListDto>>> GetFollowersAsync(Guid userId);
            Task<IDataResult<List<FollowListDto>>> GetFollowingsAsync(Guid userId);
            Task<IDataResult<bool>> IsFollowingAsync(Guid followerId, Guid followingId);
            Task<IDataResult<List<FollowListDto>>> GetTopFollowedAuthorsAsync(int count);
        }

    
}
