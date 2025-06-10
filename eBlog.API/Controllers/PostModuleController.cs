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
        [HttpGet]
        public async Task<IActionResult> GetAll()
      => Ok(await _service.GetAllAsync());

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PostModuleCreateDto dto)
            => Ok(await _service.AddAsync(dto));

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, PostModuleUpdateDto dto)
            => Ok(await _service.UpdateAsync(id, dto));

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted.Success) return NotFound();
            return NoContent();
        }
    }
}
