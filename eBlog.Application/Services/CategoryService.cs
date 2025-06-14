using AutoMapper;
using eBlog.Application.DTOs;
using eBlog.Application.Interfaces;
using eBlog.Domain.Entities;
using eBlog.Domain.Interfaces;
using eBlog.Domain.Interfaces.DAO;
using eBlog.Shared.Results;

namespace eBlog.Application.Services
{
    public class CategoryService : GenericService<Category, CategoryListDto, CategoryCreateDto, CategoryUpdateDto>, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICategoryDao _categoryDao;
        private readonly IMapper _mapper;

        public CategoryService(
            ICategoryRepository categoryRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ICategoryDao categoryDao
        ) : base(categoryRepository, unitOfWork, mapper)
        {
            _categoryRepository = categoryRepository;
            _categoryDao = categoryDao;
            _mapper = mapper;
        }

        public async Task<IDataResult<List<CategoryListDto>>> GetPopularCategoriesAsync(int count)
        {
            try
            {
                var entities = await _categoryDao.GetPopularCategoriesAsync(count);
                var dtos = _mapper.Map<List<CategoryListDto>>(entities);
                return new SuccessDataResult<List<CategoryListDto>>(dtos);
            }
            catch (Exception ex)
            {
                // Logger ile hata kaydı alınabilir
                return new ErrorDataResult<List<CategoryListDto>>("Popüler kategoriler alınırken hata oluştu: " + ex.Message);
            }
        }

        public async Task<IDataResult<List<CategoryListDto>>> GetAllWithSubCategoriesAsync()
        {
            try
            {
                var entities = await _categoryRepository.GetAllWithSubCategoriesAsync();
                var dtos = _mapper.Map<List<CategoryListDto>>(entities);
                return new SuccessDataResult<List<CategoryListDto>>(dtos);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<CategoryListDto>>("Kategoriler alınırken hata oluştu: " + ex.Message);
            }
        }
    }
}
