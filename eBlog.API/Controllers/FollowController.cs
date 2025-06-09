using eBlog.Application.DTOs;
using eBlog.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace eBlog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FollowController : ControllerBase
    {
        private readonly IFollowService _service;
        public FollowController(IFollowService service)
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
        public async Task<IActionResult> Create([FromBody] FollowCreateDto dto)
        {
            var result = await _service.AddAsync(dto);
            if (!result.Success)
                return BadRequest(result);
            return CreatedAtAction(nameof(GetById), new { id = ((dynamic)result.Data).Id }, result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] FollowCreateDto dto)
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

        // GET: api/follow/followers/{userId}
        [HttpGet("followers/{userId:guid}")]
        public async Task<IActionResult> GetFollowers(Guid userId)
        {
            var result = await _service.GetFollowersAsync(userId);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }

        // GET: api/follow/followings/{userId}
        [HttpGet("followings/{userId:guid}")]
        public async Task<IActionResult> GetFollowings(Guid userId)
        {
            var result = await _service.GetFollowingsAsync(userId);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }

        // GET: api/follow/is-following
        [HttpGet("is-following")]
        public async Task<IActionResult> IsFollowing(Guid followerId, Guid followingId)
        {
            var result = await _service.IsFollowingAsync(followerId, followingId);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }

        // GET: api/follow/top-followed-authors/{count}
        [HttpGet("top-followed-authors/{count:int}")]
        public async Task<IActionResult> GetTopFollowedAuthors(int count)
        {
            var result = await _service.GetTopFollowedAuthorsAsync(count);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }
    }
}
