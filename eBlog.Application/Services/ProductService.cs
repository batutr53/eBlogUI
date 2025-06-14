using AutoMapper;
using eBlog.Application.DTOs;
using eBlog.Application.Interfaces;
using eBlog.Domain.Entities;
using eBlog.Domain.Interfaces;
using eBlog.Domain.Interfaces.DAO;
using eBlog.Shared.Results;

namespace eBlog.Application.Services
{
    public class ProductService : GenericService<Product, ProductListDto, ProductCreateDto, ProductUpdateDto>, IProductService
    {
        private readonly IProductDao _productDao;
        private readonly IMapper _mapper;

        public ProductService(
            IGenericRepository<Product> repository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IProductDao productDao
        ) : base(repository, unitOfWork, mapper)
        {
            _productDao = productDao;
            _mapper = mapper;
        }

        public async Task<IDataResult<List<ProductListDto>>> GetPopularProductsAsync(int count)
        {
            try
            {
                var entities = await _productDao.GetPopularProductsAsync(count);
                var dtos = _mapper.Map<List<ProductListDto>>(entities);
                return new SuccessDataResult<List<ProductListDto>>(dtos);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<ProductListDto>>("Popüler ürünler getirilirken hata oluştu: " + ex.Message);
            }
        }

        // Product'a özel başka metotlar da aynı şekilde eklenebilir.
    }
}
