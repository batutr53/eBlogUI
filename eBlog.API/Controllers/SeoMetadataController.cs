using eBlog.Application.DTOs;
using eBlog.Application.Interfaces;
using eBlog.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace eBlog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SeoMetadataController : ControllerBase
    {
        private readonly ISeoMetadataService _service;
        public SeoMetadataController(ISeoMetadataService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var data = await _service.GetByIdAsync(id);
            return data == null ? NotFound() : Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SeoMetadataCreateDto dto)
            => Ok(await _service.AddAsync(dto));

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] SeoMetadataUpdateDto dto)
            => Ok(await _service.UpdateAsync(id, dto));

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _service.DeleteAsync(id);
            return deleted.Success ? NoContent() : NotFound();
        }
        [HttpGet("{postId}/variants")]
        public async Task<IActionResult> GetSeoVariantsByPost(Guid postId)
        {
            var variants = await _service.GetVariantsByPostIdAsync(postId);
            return Ok(variants);
        }
    }

}
