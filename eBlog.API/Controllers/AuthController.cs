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

        public AuthController(IUserService userService, IJwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var existingUser = await _userService.GetByEmailAsync(dto.Email);
            if (existingUser.Success && existingUser.Data != null)
                return BadRequest(new { message = "Bu email zaten kayıtlı." });

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
                return Unauthorized("Email veya şifre hatalı.");

            var user = userResult.Data;

            if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                return Unauthorized("Email veya şifre hatalı.");

            var jwtToken = _jwtService.GenerateJwtToken(user);
            var refreshToken = _jwtService.GenerateRefreshToken();

            // Refresh token'ı veritabanına kaydet
            await _userService.SaveRefreshTokenAsync(user.Id, refreshToken, Request.HttpContext.Connection.RemoteIpAddress.ToString());

            return Ok(new { token = jwtToken, refreshToken });
        }
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(string refreshToken)
        {
            var user = await _userService.GetUserByRefreshTokenAsync(refreshToken);
            if (user == null)
                return Unauthorized("Geçersiz refresh token.");

            if (user.RefreshTokens.FirstOrDefault(rt => rt.Token == refreshToken)?.IsExpired ?? true)
                return Unauthorized("Refresh token süresi dolmuş.");

            var userDto = new UserDetailDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Roles = user.UserRoles.Select(r => r.Role.Name).ToList()
            };

            var newJwt = _jwtService.GenerateJwtToken(userDto);
            var newRefreshToken = _jwtService.GenerateRefreshToken();

            await _userService.ReplaceRefreshTokenAsync(user.Id, refreshToken, newRefreshToken, HttpContext.Connection.RemoteIpAddress?.ToString());

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
