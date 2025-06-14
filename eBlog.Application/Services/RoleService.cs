using AutoMapper;
using eBlog.Application.DTOs;
using eBlog.Application.Interfaces;
using eBlog.Domain.Entities;
using eBlog.Domain.Interfaces;
using eBlog.Shared.Results;

namespace eBlog.Application.Services
{
    public class RoleService : GenericService<Role, RoleDto, RoleCreateDto, RoleUpdateDto>, IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public RoleService(
            IRoleRepository roleRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper
        ) : base(roleRepository, unitOfWork, mapper)
        {
            _roleRepository = roleRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<RoleDto?> GetByNameAsync(string name)
        {
            var role = await _roleRepository.GetByNameAsync(name);
            return _mapper.Map<RoleDto>(role);
        }

        public async Task<IDataResult<RoleDto>> FindOrCreateRoleByNameAsync(string roleName)
        {
            var role = await _roleRepository.GetByNameAsync(roleName);
            if (role == null)
            {
                role = new Role { Name = roleName };
                await _roleRepository.AddAsync(role);
                await _unitOfWork.SaveChangesAsync();
            }
            var roleDto = _mapper.Map<RoleDto>(role);
            return new SuccessDataResult<RoleDto>(roleDto, "Rol bulundu veya oluşturuldu.");
        }
    }
}
