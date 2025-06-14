using eBlog.Shared.Results;

namespace eBlog.Application.Interfaces
{
    public interface IGenericService<TEntity, TDto, TCreateDto, TUpdateDto>
    {
        Task<IDataResult<List<TDto>>> GetAllAsync();
        Task<IDataResult<TDto>> GetByIdAsync(Guid id);
        Task<IDataResult<TDto>> AddAsync(TCreateDto dto);
        Task<IDataResult<TDto>> UpdateAsync(Guid id, TUpdateDto dto);
        Task<IResult> DeleteAsync(Guid id);

    }
}
