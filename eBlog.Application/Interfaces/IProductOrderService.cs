using eBlog.Application.DTOs;
using eBlog.Domain.Entities;
using eBlog.Domain.Interfaces;
using eBlog.Shared.Results;

namespace eBlog.Application.Interfaces
{
    public interface IProductOrderService : IGenericService<ProductOrder,ProductOrderListDto, ProductOrderCreateDto, ProductOrderCreateDto>
    {
        Task<IDataResult<List<ProductOrderListDto>>> GetOrdersByBuyerIdAsync(Guid buyerId);
        Task<IDataResult<List<ProductOrderListDto>>> GetOrdersByProductIdAsync(Guid productId);
        Task<IDataResult<decimal>> GetTotalSalesForProductAsync(Guid productId);

    }
}
