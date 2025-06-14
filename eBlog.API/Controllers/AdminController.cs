using eBlog.Application.DTOs;
using eBlog.Application.DTOs.Auth;
using eBlog.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eBlog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")] 
    public class AdminController : ControllerBase
    {
        private readonly IUserService _userService;

        public AdminController(IUserService userService)
        {
            _userService = userService;
        }

        // POST: api/admin/add-role
        [HttpPost("add-role")]
        public async Task<IActionResult> AddRole(UserRoleUpdateDto dto)
        {
            var result = await _userService.AddRoleToUserAsync(dto);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }

        // POST: api/admin/remove-role
        [HttpPost("remove-role")]
        public async Task<IActionResult> RemoveRole(UserRoleUpdateDto dto)
        {
            var result = await _userService.RemoveRoleFromUserAsync(dto);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPost("update-roles")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateRoles(Guid userId, [FromBody] List<string> roles)
        {
            var result = await _userService.UpdateRolesAndGenerateJwtAsync(userId, roles);
            if (!result.Success)
                return BadRequest(result);

            return Ok(new { token = result.Data, message = result.Message });
        }

    }
}
