using eBlog.Application.DTOs;
using eBlog.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace eBlog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FavoriteController : ControllerBase
    {
        private readonly IFavoriteService _service;
        public FavoriteController(IFavoriteService service)
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
        public async Task<IActionResult> Create([FromBody] FavoriteCreateDto dto)
        {
            var result = await _service.AddAsync(dto);
            if (!result.Success)
                return BadRequest(result);
            return CreatedAtAction(nameof(GetById), new { id = ((dynamic)result.Data).Id }, result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] FavoriteCreateDto dto)
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

        // GET: api/favorite/user/{userId}
        [HttpGet("user/{userId:guid}")]
        public async Task<IActionResult> GetByUserId(Guid userId)
        {
            var result = await _service.GetFavoritesByUserIdAsync(userId);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }

        // GET: api/favorite/is-favorited
        [HttpGet("is-favorited")]
        public async Task<IActionResult> IsFavorited(Guid userId, Guid? postId, Guid? productId, Guid? commentId)
        {
            var result = await _service.IsFavoritedAsync(userId, postId, productId, commentId);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }

        // GET: api/favorite/most-favorited-posts/{count}
        [HttpGet("most-favorited-posts/{count:int}")]
        public async Task<IActionResult> GetMostFavoritedPosts(int count)
        {
            var result = await _service.GetMostFavoritedPostsAsync(count);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }
    }
}
