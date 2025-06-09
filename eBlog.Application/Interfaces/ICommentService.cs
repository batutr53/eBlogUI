using eBlog.Application.DTOs;
using eBlog.Domain.Entities;
using eBlog.Domain.Interfaces;
using eBlog.Shared.Results;

namespace eBlog.Application.Interfaces
{
    public interface ICommentService : IGenericService<Comment,CommentListDto, CommentCreateDto, CommentCreateDto>
    {
        Task<IDataResult<List<CommentListDto>>> GetCommentsByPostIdAsync(Guid postId);
        Task<IDataResult<List<CommentListDto>>> GetCommentsByProductIdAsync(Guid productId);
        Task<IDataResult<List<CommentListDto>>> GetRecentCommentsAsync(int count);

    }
}
