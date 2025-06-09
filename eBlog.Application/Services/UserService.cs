using AutoMapper;
using eBlog.Application.DTOs;
using eBlog.Application.Interfaces;
using eBlog.Domain.Entities;
using eBlog.Domain.Interfaces;
using eBlog.Domain.Interfaces.DAO;
using eBlog.Shared.Results;

namespace eBlog.Application.Services
{
    public class UserService : GenericService<User, UserListDto, UserCreateDto, UserUpdateDto>, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserDao _userDao;
        private readonly IMapper _mapper;

        public UserService(
            IUserRepository userRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IUserDao userDao
        ) : base(userRepository, unitOfWork, mapper)
        {
            _userRepository = userRepository;
            _userDao = userDao;
            _mapper = mapper;
        }

        public async Task<IDataResult<UserDetailDto>> GetByEmailAsync(string email)
        {
            try
            {
                var entity = await _userRepository.GetByEmailAsync(email);
                if (entity == null)
                    return new ErrorDataResult<UserDetailDto>("Kullanıcı bulunamadı.");
                var dto = _mapper.Map<UserDetailDto>(entity);
                return new SuccessDataResult<UserDetailDto>(dto);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<UserDetailDto>("Kullanıcı aranırken hata oluştu: " + ex.Message);
            }
        }

        public async Task<IDataResult<List<UserListDto>>> GetActiveAuthorsAsync(int count)
        {
            try
            {
                var entities = await _userDao.GetActiveAuthorsAsync(count);
                var dtos = _mapper.Map<List<UserListDto>>(entities);
                return new SuccessDataResult<List<UserListDto>>(dtos);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<UserListDto>>("Aktif yazarlar alınırken hata oluştu: " + ex.Message);
            }
        }
    }
}
