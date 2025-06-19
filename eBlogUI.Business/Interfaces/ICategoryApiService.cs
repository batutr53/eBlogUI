using eBlogUI.Models.Dtos;
using eBlogUI.Models.Dtos.Category;
using eBlog.Shared.Results;

namespace eBlogUI.Business.Interfaces
{
    public interface ICategoryApiService
    {
        Task<IDataResult<List<CategoryListDto>>> GetListAsync();
        Task<IDataResult<CategoryListDto>> GetByIdAsync(Guid id);
        Task<IDataResult<CategoryListDto>> GetBySlugAsync(string slug);
        Task<IResult> CreateAsync(CategoryCreateDto dto);
        Task<IResult> UpdateAsync(CategoryUpdateDto dto);
        Task<IResult> DeleteAsync(Guid id);
        Task<IDataResult<int>> GetPostCountByCategoryAsync(Guid categoryId);
    }
}
