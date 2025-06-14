using eBlog.Application.DTOs;
using eBlog.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace eBlog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _service;
        public NotificationController(INotificationService service)
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
        public async Task<IActionResult> Create([FromBody] NotificationDto dto)
        {
            var result = await _service.AddAsync(dto);
            if (!result.Success)
                return BadRequest(result);
            return CreatedAtAction(nameof(GetById), new { id = ((dynamic)result.Data).Id }, result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] NotificationDto dto)
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

        // GET: api/notification/unread/{userId}
        [HttpGet("unread/{userId:guid}")]
        public async Task<IActionResult> GetUnreadNotificationsByUserId(Guid userId)
        {
            var result = await _service.GetUnreadNotificationsByUserIdAsync(userId);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }

        // POST: api/notification/mark-all-read/{userId}
        [HttpPost("mark-all-read/{userId:guid}")]
        public async Task<IActionResult> MarkAllAsRead(Guid userId)
        {
            var result = await _service.MarkAllAsReadAsync(userId);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }

        // GET: api/notification/unread-count/{userId}
        [HttpGet("unread-count/{userId:guid}")]
        public async Task<IActionResult> GetUnreadCountByUserId(Guid userId)
        {
            var result = await _service.GetUnreadCountByUserIdAsync(userId);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }
    }
}
