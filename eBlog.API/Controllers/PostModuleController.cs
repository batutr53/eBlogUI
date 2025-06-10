using eBlog.Application.DTOs;
using eBlog.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace eBlog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostModuleController : ControllerBase
    {
        private readonly IPostModuleService _service;
        public PostModuleController(IPostModuleService service) => _service = service;

        [HttpGet("{postId}")]
        public async Task<IActionResult> GetModules(Guid postId)
            => Ok(await _service.GetModulesByPostIdAsync(postId));

        [HttpPut("{postId}")]
        public async Task<IActionResult> UpdateModules(Guid postId, [FromBody] List<PostModuleDto> modules)
        {
            await _service.UpdatePostModulesAsync(postId, modules);
            return NoContent();
        }
    }
}
