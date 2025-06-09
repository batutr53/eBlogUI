using eBlog.Application.DTOs;
using eBlog.Domain.Entities;
using eBlog.Domain.Interfaces;
using eBlog.Shared.Results;

namespace eBlog.Application.Interfaces
{
    public interface ICategoryService : IGenericService<Category,CategoryListDto, CategoryCreateDto, CategoryUpdateDto>
    {
        Task<IDataResult<List<CategoryListDto>>> GetPopularCategoriesAsync(int count);
        Task<IDataResult<List<CategoryListDto>>> GetAllWithSubCategoriesAsync();

    }
}
