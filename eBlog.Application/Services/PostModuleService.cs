using AutoMapper;
using eBlog.Application.DTOs;
using eBlog.Application.Interfaces;
using eBlog.Domain.Entities;
using eBlog.Domain.Interfaces;

namespace eBlog.Application.Services
{
    public class PostModuleService : IPostModuleService
    {
        private readonly IPostModuleRepository _repo;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;

        public PostModuleService(IPostModuleRepository repo, IMapper mapper, IUnitOfWork uow)
        {
            _repo = repo;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<List<PostModuleDto>> GetModulesByPostIdAsync(Guid postId)
        {
            var modules = await _repo.GetModulesByPostIdAsync(postId);
            return _mapper.Map<List<PostModuleDto>>(modules);
        }

        public async Task UpdatePostModulesAsync(Guid postId, List<PostModuleDto> modules)
        {
            await _repo.DeleteModulesByPostIdAsync(postId);

            var entities = _mapper.Map<List<PostModule>>(modules);
            foreach (var item in entities)
            {
                item.Id = Guid.NewGuid();
                item.PostId = postId;
            }

            await _repo.AddRangeAsync(entities);
            await _uow.SaveChangesAsync();
        }
    }
}
