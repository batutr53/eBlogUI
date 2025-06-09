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
    }
}
