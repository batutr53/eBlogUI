using eBlog.Domain.Entities;

namespace eBlog.Domain.Interfaces.DAO
{
    public interface ITagDao
    {
        Task<List<Tag>> GetMostUsedTagsAsync(int count);
        // Taga özel dapper metotlar
    }
}
