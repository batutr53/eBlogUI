using eBlog.Domain.Entities;

namespace eBlog.Domain.Interfaces.DAO
{

    public interface ICategoryDao
    {
        Task<List<Category>> GetPopularCategoriesAsync(int count);
        // Kategoriye özel dapper metotlar
    }
}
