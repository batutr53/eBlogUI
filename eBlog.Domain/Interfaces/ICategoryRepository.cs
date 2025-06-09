using eBlog.Domain.Entities;

namespace eBlog.Domain.Interfaces
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<List<Category>> GetAllWithSubCategoriesAsync();
        // Kategoriye özel ekstra metotlar eklenebilir.
    }
}
