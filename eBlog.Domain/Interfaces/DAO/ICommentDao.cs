using eBlog.Domain.Entities;

namespace eBlog.Domain.Interfaces.DAO
{
    public interface ICommentDao
    {
        Task<List<Comment>> GetRecentCommentsAsync(int count);
        // Yorumlara özel dapper metotlar
    }
}
