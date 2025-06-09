using eBlog.Application.DTOs;
using eBlog.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace eBlog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _service;
        public CommentController(ICommentService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _service.GetByIdAsync(id);
            if (!result.Success)
                return NotFound(result);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CommentCreateDto dto)
        {
            var result = await _service.AddAsync(dto);
            if (!result.Success)
                return BadRequest(result);
            return CreatedAtAction(nameof(GetById), new { id = ((dynamic)result.Data).Id }, result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CommentCreateDto dto)
        {
            var result = await _service.UpdateAsync(id, dto);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _service.DeleteAsync(id);
            if (!result.Success)
                return NotFound(result);
            return Ok(result);
        }

        // GET: api/comment/post/{postId}
        [HttpGet("post/{postId:guid}")]
        public async Task<IActionResult> GetByPostId(Guid postId)
        {
            var result = await _service.GetCommentsByPostIdAsync(postId);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }

        // GET: api/comment/product/{productId}
        [HttpGet("product/{productId:guid}")]
        public async Task<IActionResult> GetByProductId(Guid productId)
        {
            var result = await _service.GetCommentsByProductIdAsync(productId);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }

        // GET: api/comment/recent/{count}
        [HttpGet("recent/{count:int}")]
        public async Task<IActionResult> GetRecent(int count)
        {
            var result = await _service.GetRecentCommentsAsync(count);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }
    }
}
