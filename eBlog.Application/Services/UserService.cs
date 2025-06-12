using AutoMapper;
using eBlog.Application.DTOs;
using eBlog.Application.DTOs.Auth;
using eBlog.Application.Interfaces;
using eBlog.Domain.Entities;
using eBlog.Domain.Interfaces;
using eBlog.Domain.Interfaces.DAO;
using eBlog.Shared.Results;

namespace eBlog.Application.Services
{
    public class UserService : GenericService<User, UserListDto, UserCreateDto, UserUpdateDto>, IUserService
    {
        private readonly IJwtService _jwtService;
        private readonly IUserRepository _userRepository;
        private readonly IUserDao _userDao;
        private readonly IMapper _mapper;
        private readonly IRoleRepository _roleRepository;

        public UserService(
            IUserRepository userRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IUserDao userDao,
            IJwtService jwtService,
            IRoleRepository roleRepository
        ) : base(userRepository, unitOfWork, mapper)
        {
            _userRepository = userRepository;
            _userDao = userDao;
            _mapper = mapper;
            _jwtService = jwtService;
            _roleRepository = roleRepository;
        }

        public async Task<IResult> AddRoleToUserAsync(UserRoleUpdateDto dto)
        {
            var user = await _userRepository.GetByIdWithRolesAsync(dto.UserId);
            if (user == null)
                return new ErrorResult("Kullanıcı bulunamadı.");

            var role = await _roleRepository.GetByNameAsync(dto.RoleName);
            if (role == null)
                return new ErrorResult("Rol bulunamadı.");

            if (user.UserRoles.Any(r => r.RoleId == role.Id))
                return new ErrorResult("Kullanıcı bu role zaten sahip.");

            user.UserRoles.Add(new UserRole
            {
                UserId = user.Id,
                RoleId = role.Id
            });

            await _unitOfWork.SaveChangesAsync();
            return new SuccessResult("Rol başarıyla eklendi.");
        }

        public async Task<IResult> RemoveRoleFromUserAsync(UserRoleUpdateDto dto)
        {
            var user = await _userRepository.GetByIdWithRolesAsync(dto.UserId);
            if (user == null)
                return new ErrorResult("Kullanıcı bulunamadı.");

            var userRole = user.UserRoles.FirstOrDefault(r => r.Role != null && r.Role.Name == dto.RoleName);
            if (userRole == null)
                return new ErrorResult("Kullanıcı bu role sahip değil.");

            user.UserRoles.Remove(userRole);
            await _unitOfWork.SaveChangesAsync();

            return new SuccessResult("Rol kaldırıldı.");
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

        public async Task<IDataResult<string>> UpdateRolesAndGenerateJwtAsync(Guid userId, List<string> roles)
        {
            var user = await _userRepository.GetByIdWithRolesAsync(userId);
            if (user == null)
                return new ErrorDataResult<string>("Kullanıcı bulunamadı.");

            user.UserRoles.Clear();

            foreach (var roleName in roles)
            {
                var role = await _roleRepository.GetByNameAsync(roleName);
                if (role == null)
                    return new ErrorDataResult<string>($"Rol bulunamadı: {roleName}");

                user.UserRoles.Add(new UserRole { UserId = userId, RoleId = role.Id });
            }

            await _unitOfWork.SaveChangesAsync();

            var userDto = new UserDetailDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Roles = roles
            };

            var jwtToken = _jwtService.GenerateJwtToken(userDto);

            return new SuccessDataResult<string>(jwtToken, "Roller güncellendi, JWT oluşturuldu.");
        }

        public async Task<IResult> GeneratePasswordResetTokenAsync(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null)
                return new ErrorResult("Kullanıcı bulunamadı.");

            var resetToken = Guid.NewGuid().ToString();
            user.PasswordResetToken = resetToken;
            user.ResetTokenExpires = DateTime.UtcNow.AddHours(1);

            await _unitOfWork.SaveChangesAsync();
            return new SuccessResult("Şifre yenileme linki e-postanıza gönderildi.");
        }

        public async Task<IResult> ResetPasswordAsync(string token, string newPassword)
        {
            var user = await _userRepository.GetByResetTokenAsync(token);
            if (user == null || user.ResetTokenExpires < DateTime.UtcNow)
                return new ErrorResult("Geçersiz veya süre dolmuş token.");

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
            user.PasswordResetToken = null;
            user.ResetTokenExpires = null;

            await _unitOfWork.SaveChangesAsync();
            return new SuccessResult("Şifre başarıyla yenilendi.");
        }

        public async Task<IResult> VerifyEmailAsync(string token)
        {
            var user = await _userRepository.GetByEmailVerificationTokenAsync(token);
            if (user == null)
                return new ErrorResult("Geçersiz token.");

            user.IsEmailVerified = true;
            user.EmailVerificationToken = null;
            await _unitOfWork.SaveChangesAsync();

            return new SuccessResult("E-posta başarıyla doğrulandı.");
        }

        public async Task<IResult> SaveRefreshTokenAsync(Guid userId, string refreshToken, string ipAddress)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                return new ErrorResult("Kullanıcı bulunamadı.");

            var token = new RefreshToken
            {
                UserId = userId,
                Token = refreshToken,
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow,
            };

            user.RefreshTokens.Add(token);
            await _unitOfWork.SaveChangesAsync();

            return new SuccessResult("Refresh token kaydedildi.");
        }

        public async Task<User> GetUserByRefreshTokenAsync(string refreshToken)
        {
            return await _userRepository.GetByRefreshTokenAsync(refreshToken);
        }

        public async Task<IResult> ReplaceRefreshTokenAsync(Guid userId, string oldToken, string newToken, string ipAddress)
        {
            var user = await _userRepository.GetByIdWithRefreshTokensAsync(userId);
            var oldRefreshToken = user?.RefreshTokens.FirstOrDefault(rt => rt.Token == oldToken);

            if (oldRefreshToken == null)
                return new ErrorResult("Eski refresh token bulunamadı.");

            user.RefreshTokens.Remove(oldRefreshToken);
            user.RefreshTokens.Add(new RefreshToken
            {
                UserId = userId,
                Token = newToken,
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow,
            });

            await _unitOfWork.SaveChangesAsync();
            return new SuccessResult("Refresh token güncellendi.");
        }
    }
}
