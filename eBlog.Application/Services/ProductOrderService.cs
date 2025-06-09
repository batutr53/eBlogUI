using AutoMapper;
using eBlog.Application.DTOs;
using eBlog.Application.Interfaces;
using eBlog.Domain.Entities;
using eBlog.Domain.Interfaces;
using eBlog.Domain.Interfaces.DAO;
using eBlog.Shared.Results;

namespace eBlog.Application.Services
{
    public class ProductOrderService : GenericService<ProductOrder, ProductOrderListDto, ProductOrderCreateDto, ProductOrderCreateDto>, IProductOrderService
    {
        private readonly IProductOrderRepository _productOrderRepository;
        private readonly IProductOrderDao _productOrderDao;
        private readonly IMapper _mapper;

        public ProductOrderService(
            IProductOrderRepository productOrderRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IProductOrderDao productOrderDao
        ) : base(productOrderRepository, unitOfWork, mapper)
        {
            _productOrderRepository = productOrderRepository;
            _productOrderDao = productOrderDao;
            _mapper = mapper;
        }

        public async Task<IDataResult<List<ProductOrderListDto>>> GetOrdersByBuyerIdAsync(Guid buyerId)
        {
            try
            {
                var entities = await _productOrderRepository.GetOrdersByBuyerIdAsync(buyerId);
                var dtos = _mapper.Map<List<ProductOrderListDto>>(entities);
                return new SuccessDataResult<List<ProductOrderListDto>>(dtos);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<ProductOrderListDto>>("Kullanıcıya ait siparişler getirilirken hata oluştu: " + ex.Message);
            }
        }

        public async Task<IDataResult<List<ProductOrderListDto>>> GetOrdersByProductIdAsync(Guid productId)
        {
            try
            {
                var entities = await _productOrderRepository.GetOrdersByProductIdAsync(productId);
                var dtos = _mapper.Map<List<ProductOrderListDto>>(entities);
                return new SuccessDataResult<List<ProductOrderListDto>>(dtos);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<ProductOrderListDto>>("Ürüne ait siparişler getirilirken hata oluştu: " + ex.Message);
            }
        }

        public async Task<IDataResult<decimal>> GetTotalSalesForProductAsync(Guid productId)
        {
            try
            {
                var total = await _productOrderDao.GetTotalSalesForProductAsync(productId);
                return new SuccessDataResult<decimal>(total);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<decimal>("Ürünün toplam satışları alınırken hata oluştu: " + ex.Message);
            }
        }
    }
}
