using AutoMapper;
using eBlog.Application.DTOs;
using eBlog.Application.Interfaces;
using eBlog.Domain.Entities;
using eBlog.Domain.Interfaces;

namespace eBlog.Application.Services
{
    public class SeoMetadataService : GenericService<SeoMetadata, SeoMetadataDto, SeoMetadataCreateDto, SeoMetadataUpdateDto>, ISeoMetadataService
    {
        private readonly IGenericRepository<SeoMetadata> _repository;
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public SeoMetadataService(
            IGenericRepository<SeoMetadata> repository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IPostRepository postRepository
        ) : base(repository, unitOfWork, mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _postRepository = postRepository;
        }

        public async Task<List<SeoMetadataDto>> GetVariantsByPostIdAsync(Guid postId)
        {
            var post = await _postRepository.GetByIdAsync(postId);
            if (post == null || post.SeoMetadataId == null)
                return new List<SeoMetadataDto>();

            var canonicalSeo = await _repository.GetByIdAsync(post.SeoMetadataId.Value);
            if (canonicalSeo == null)
                return new List<SeoMetadataDto>();

            var variants = await _repository.GetAllAsync(
                x => x.CanonicalGroupId == canonicalSeo.CanonicalGroupId
            );

            return _mapper.Map<List<SeoMetadataDto>>(variants);
        }
    }
}
