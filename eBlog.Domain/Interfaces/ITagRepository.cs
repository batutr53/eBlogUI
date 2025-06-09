using eBlog.Domain.Entities;

namespace eBlog.Domain.Interfaces
{
    public interface ITagRepository : IGenericRepository<Tag>
    {
        Task<Tag?> GetBySlugAsync(string slug);
        // Taga özel ekstra metotlar eklenebilir.
    }
}
