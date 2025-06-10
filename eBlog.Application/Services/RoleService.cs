using AutoMapper;
using eBlog.Application.DTOs;
using eBlog.Application.Interfaces;
using eBlog.Domain.Entities;
using eBlog.Domain.Interfaces;
using eBlog.Shared.Results;

namespace eBlog.Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public RoleService(IRoleRepository roleRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<RoleListDto>> GetAllAsync()
        {
            var roles = await _roleRepository.GetAllAsync();
            return _mapper.Map<List<RoleListDto>>(roles);
        }

        public async Task<RoleDto?> GetByIdAsync(Guid id)
        {
            var role = await _roleRepository.GetByIdAsync(id);
            return _mapper.Map<RoleDto>(role);
        }

        public async Task<RoleDto?> GetByNameAsync(string name)
        {
            var role = await _roleRepository.GetByNameAsync(name);
            return _mapper.Map<RoleDto>(role);
        }

        public async Task<IResult> AddAsync(RoleCreateDto dto)
        {
            if ((await _roleRepository.GetByNameAsync(dto.Name)) is not null)
                return new ErrorResult("Rol zaten mevcut.");

            var entity = _mapper.Map<Role>(dto);
            await _roleRepository.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return new SuccessResult("Rol eklendi.");
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
