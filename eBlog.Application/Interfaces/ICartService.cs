using eBlog.Application.DTOs;
using eBlog.Domain.Entities;
using eBlog.Domain.Interfaces;
using eBlog.Shared.Results;

namespace eBlog.Application.Interfaces
{

    public interface ICartService : IGenericService<Cart,CartDto, CartDto, CartDto>
    {
        Task<IDataResult<CartDto>> GetCartByUserIdAsync(Guid userId);
        Task<IDataResult<decimal>> GetTotalCartPriceAsync(Guid cartId);
        Task<IDataResult<List<CartItemDto>>> GetCartItemsAsync(Guid userId);
        Task<IResult> AddToCartAsync(Guid userId, AddToCartDto dto);
        Task<IResult> RemoveFromCartAsync(Guid userId, Guid productId);
        Task<IResult> ClearCartAsync(Guid userId);
    }
}
