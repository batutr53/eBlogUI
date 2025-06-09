using AutoMapper;
using eBlog.Application.Interfaces;
using eBlog.Domain.Interfaces;
using eBlog.Shared.Results;

namespace eBlog.Application.Services
{
    public class GenericService<TEntity, TDto, TCreateDto, TUpdateDto> : IGenericService<TEntity, TDto, TCreateDto, TUpdateDto>
        where TEntity : class, IEntity, new()
    {
        protected readonly IGenericRepository<TEntity> _repository;
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;

        public GenericService(IGenericRepository<TEntity> repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public virtual async Task<IDataResult<List<TDto>>> GetAllAsync()
        {
            try
            {
                var entities = await _repository.GetAllAsync();
                var dtos = _mapper.Map<List<TDto>>(entities);
                return new SuccessDataResult<List<TDto>>(dtos);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<TDto>>("Liste alınırken hata oluştu: " + ex.Message);
            }
        }

        public virtual async Task<IDataResult<TDto>> GetByIdAsync(Guid id)
        {
            try
            {
                var entity = await _repository.GetByIdAsync(id);
                if (entity == null)
                    return new ErrorDataResult<TDto>("Kayıt bulunamadı.");
                var dto = _mapper.Map<TDto>(entity);
                return new SuccessDataResult<TDto>(dto);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<TDto>("Kayıt alınırken hata oluştu: " + ex.Message);
            }
        }

        public virtual async Task<IDataResult<TDto>> AddAsync(TCreateDto dto)
        {
            try
            {
                var entity = _mapper.Map<TEntity>(dto);
                await _repository.AddAsync(entity);
                await _unitOfWork.SaveChangesAsync();
                var resultDto = _mapper.Map<TDto>(entity);
                return new SuccessDataResult<TDto>(resultDto, "Başarıyla eklendi.");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<TDto>("Ekleme sırasında hata oluştu: " + ex.Message);
            }
        }

        public virtual async Task<IDataResult<TDto>> UpdateAsync(Guid id, TUpdateDto dto)
        {
            try
            {
                var entity = await _repository.GetByIdAsync(id);
                if (entity == null)
                    return new ErrorDataResult<TDto>("Güncellenecek kayıt bulunamadı.");
                _mapper.Map(dto, entity);
                _repository.Update(entity);
                await _unitOfWork.SaveChangesAsync();
                var resultDto = _mapper.Map<TDto>(entity);
                return new SuccessDataResult<TDto>(resultDto, "Başarıyla güncellendi.");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<TDto>("Güncelleme sırasında hata oluştu: " + ex.Message);
            }
        }

        public virtual async Task<IResult> DeleteAsync(Guid id)
        {
            try
            {
                var entity = await _repository.GetByIdAsync(id);
                if (entity == null)
                    return new ErrorResult("Silinecek kayıt bulunamadı.");
                _repository.Remove(entity);
                await _unitOfWork.SaveChangesAsync();
                return new SuccessResult("Başarıyla silindi.");
            }
            catch (Exception ex)
            {
                return new ErrorResult("Silme sırasında hata oluştu: " + ex.Message);
            }
        }
    }
}
