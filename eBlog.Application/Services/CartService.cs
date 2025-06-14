using AutoMapper;
using eBlog.Application.DTOs;
using eBlog.Application.Interfaces;
using eBlog.Domain.Entities;
using eBlog.Domain.Interfaces;
using eBlog.Domain.Interfaces.DAO;
using eBlog.Shared.Results;

namespace eBlog.Application.Services
{
    public class CartService : GenericService<Cart, CartDto, CartDto, CartDto>, ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly ICartDao _cartDao;
        private readonly IMapper _mapper;

        public CartService(
            ICartRepository cartRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ICartDao cartDao
        ) : base(cartRepository, unitOfWork, mapper)
        {
            _cartRepository = cartRepository;
            _cartDao = cartDao;
            _mapper = mapper;
        }

        public async Task<IDataResult<CartDto>> GetCartByUserIdAsync(Guid userId)
        {
            try
            {
                var entity = await _cartRepository.GetCartByUserIdAsync(userId);
                if (entity == null)
                    return new ErrorDataResult<CartDto>("Sepet bulunamadı.");

                var dto = _mapper.Map<CartDto>(entity);
                return new SuccessDataResult<CartDto>(dto);
            }
            catch (Exception ex)
            {
                // Loglama ekleyebilirsin
                return new ErrorDataResult<CartDto>("Beklenmeyen bir hata oluştu: " + ex.Message);
            }
        }

        public async Task<IDataResult<decimal>> GetTotalCartPriceAsync(Guid cartId)
        {
            try
            {
                var price = await _cartDao.GetTotalCartPriceAsync(cartId);
                return new SuccessDataResult<decimal>(price);
            }
            catch (Exception ex)
            {
                // Loglama ekleyebilirsin
                return new ErrorDataResult<decimal>("Toplam sepet tutarı hesaplanamadı: " + ex.Message);
            }
        }

        public async Task<IResult> AddToCartAsync(Guid userId, AddToCartDto dto)
        {
            var cart = await _cartRepository.GetByUserIdWithItemsAsync(userId);
            if (cart == null)
            {
                cart = new Cart { UserId = userId };
                await _cartRepository.AddAsync(cart);
            }

            var existingItem = cart.CartItems.FirstOrDefault(x => x.ProductId == dto.ProductId);
            if (existingItem != null)
            {
                existingItem.Quantity += dto.Quantity;
            }
            else
            {
                cart.CartItems.Add(new CartItem
                {
                    ProductId = dto.ProductId,
                    Quantity = dto.Quantity
                });
            }

            await _unitOfWork.SaveChangesAsync();
            return new SuccessResult("Ürün sepete eklendi.");
        }

        public async Task<IDataResult<List<CartItemDto>>> GetCartItemsAsync(Guid userId)
        {
            var cart = await _cartRepository.GetByUserIdWithItemsAsync(userId);
            if (cart == null || !cart.CartItems.Any())
                return new SuccessDataResult<List<CartItemDto>>(new List<CartItemDto>(), "Sepet boş.");

            var items = cart.CartItems.Select(i => new CartItemDto
            {
                ProductId = i.ProductId,
                Quantity = i.Quantity,
                ProductName = i.Product.Name,
                UnitPrice = i.Product.Price
            }).ToList();

            return new SuccessDataResult<List<CartItemDto>>(items);
        }

        public async Task<IResult> RemoveFromCartAsync(Guid userId, Guid productId)
        {
            var cart = await _cartRepository.GetByUserIdWithItemsAsync(userId);
            if (cart == null) return new ErrorResult("Sepet bulunamadı.");

            var item = cart.CartItems.FirstOrDefault(i => i.ProductId == productId);
            if (item == null) return new ErrorResult("Ürün sepette yok.");

            cart.CartItems.Remove(item);
            await _unitOfWork.SaveChangesAsync();

            return new SuccessResult("Ürün sepetten çıkarıldı.");
        }

        public async Task<IResult> ClearCartAsync(Guid userId)
        {
            var cart = await _cartRepository.GetByUserIdWithItemsAsync(userId);
            if (cart == null) return new ErrorResult("Sepet bulunamadı.");

            cart.CartItems.Clear();
            await _unitOfWork.SaveChangesAsync();

            return new SuccessResult("Sepet temizlendi.");
        }
    }
}
