using AutoMapper;
using eBlog.Application.DTOs;
using eBlog.Application.Interfaces;
using eBlog.Domain.Entities;
using eBlog.Domain.Interfaces;
using eBlog.Domain.Interfaces.DAO;
using eBlog.Shared.Results;

namespace eBlog.Application.Services
{
    public class TagService : GenericService<Tag, TagListDto, TagCreateDto, TagCreateDto>, ITagService
    {
        private readonly ITagRepository _tagRepository;
        private readonly ITagDao _tagDao;
        private readonly IMapper _mapper;

        public TagService(
            ITagRepository tagRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ITagDao tagDao
        ) : base(tagRepository, unitOfWork, mapper)
        {
            _tagRepository = tagRepository;
            _tagDao = tagDao;
            _mapper = mapper;
        }

        public async Task<IDataResult<List<TagListDto>>> GetMostUsedTagsAsync(int count)
        {
            try
            {
                var entities = await _tagDao.GetMostUsedTagsAsync(count);
                var dtos = _mapper.Map<List<TagListDto>>(entities);
                return new SuccessDataResult<List<TagListDto>>(dtos);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<TagListDto>>("En çok kullanılan tagler getirilirken hata oluştu: " + ex.Message);
            }
        }

        public async Task<IDataResult<TagDetailDto>> GetBySlugAsync(string slug)
        {
            try
            {
                var tag = await _tagRepository.GetBySlugAsync(slug);
                if (tag == null)
                    return new ErrorDataResult<TagDetailDto>("Tag bulunamadı.");
                var dto = _mapper.Map<TagDetailDto>(tag);
                return new SuccessDataResult<TagDetailDto>(dto);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<TagDetailDto>("Tag detay alınırken hata oluştu: " + ex.Message);
            }
        }
    }
}
