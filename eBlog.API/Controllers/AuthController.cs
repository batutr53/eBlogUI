using eBlog.Application.DTOs;
using eBlog.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BCrypt.Net;
using eBlog.Application.DTOs.Auth;

namespace eBlog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;   
        private readonly IRoleService _roleService;

        public AuthController(IUserService userService, IJwtService jwtService, IRoleService roleService)
        {
            _userService = userService;
            _jwtService = jwtService;
            _roleService = roleService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var existingUser = await _userService.GetByEmailAsync(dto.Email);
            if (existingUser.Success && existingUser.Data != null)
                return BadRequest(new { message = "Bu email zaten kayıtlı." });

            // Artık rolü bulma veya oluşturma işini servise devrediyoruz.
            await _roleService.FindOrCreateRoleByNameAsync("User");

            var userCreateDto = new UserCreateDto
            {
                UserName = dto.UserName,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Roles = new List<string> { "User" } // Default rol
            };

            var result = await _userService.AddAsync(userCreateDto);
            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var userResult = await _userService.GetByEmailAsync(dto.Email);
            if (!userResult.Success || userResult.Data == null)
                return Unauthorized(new { message = "Email veya şifre hatalı." });

            var user = userResult.Data;

            if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                return Unauthorized(new { message = "Email veya şifre hatalı." });

            var jwtToken = _jwtService.GenerateJwtToken(user);
            var refreshToken = _jwtService.GenerateRefreshToken();

            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";
            await _userService.SaveRefreshTokenAsync(user.Id, refreshToken, ipAddress);

            return Ok(new { token = jwtToken, refreshToken = refreshToken });
        }
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] string refreshToken)
        {
            var user = await _userService.GetUserByRefreshTokenAsync(refreshToken);
            if (user == null)
                return Unauthorized(new { message = "Geçersiz refresh token." });

            var oldToken = user.RefreshTokens.FirstOrDefault(rt => rt.Token == refreshToken);

            if (oldToken == null || oldToken.IsExpired)
                return Unauthorized(new { message = "Refresh token süresi dolmuş veya geçersiz." });

            var userRoles = user.UserRoles.Select(ur => ur.Role.Name).ToList();

            var userDto = new UserDetailDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Roles = userRoles
            };

            var newJwt = _jwtService.GenerateJwtToken(userDto);
            var newRefreshToken = _jwtService.GenerateRefreshToken();

            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";
            await _userService.ReplaceRefreshTokenAsync(user.Id, refreshToken, newRefreshToken, ipAddress);

            return Ok(new { token = newJwt, refreshToken = newRefreshToken });
        }

        [HttpGet("verify-email")]
        public async Task<IActionResult> VerifyEmail(string token)
        {
            var result = await _userService.VerifyEmailAsync(token);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }
    }
}
