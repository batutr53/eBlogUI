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

    }
}
