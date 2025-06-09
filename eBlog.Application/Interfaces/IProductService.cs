using eBlog.Application.DTOs;
using eBlog.Domain.Entities;
using eBlog.Domain.Interfaces;
using eBlog.Shared.Results;

namespace eBlog.Application.Interfaces
{
    public interface IProductService : IGenericService<Product,ProductListDto, ProductCreateDto, ProductUpdateDto>
    {
        Task<IDataResult<List<ProductListDto>>> GetPopularProductsAsync(int count);

    }
}
