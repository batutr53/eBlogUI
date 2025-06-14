using eBlog.Application.DTOs;
using eBlog.Application.Extensions;
using eBlog.Application.Interfaces;
using eBlog.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace eBlog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductOrderController : ControllerBase
    {
        private readonly IProductOrderService _service;
        public ProductOrderController(IProductOrderService service)
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
        public async Task<IActionResult> Create([FromBody] ProductOrderCreateDto dto)
        {
            var buyerId = User.GetUserId();
            if (buyerId == Guid.Empty)
                return Unauthorized("Kullanıcı oturumu geçersiz.");

            dto.BuyerId = buyerId;
            dto.TotalPrice = dto.UnitPrice * dto.Quantity;
            dto.OrderedAt = DateTime.UtcNow;
            dto.Status = "pending";
            dto.OrderDate = DateTime.UtcNow;

            var result = await _service.AddAsync(dto);
            if (!result.Success)
                return BadRequest(result);
            return CreatedAtAction(nameof(GetById), new { id = ((dynamic)result.Data).Id }, result);
        }



        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] ProductOrderCreateDto dto)
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

        // GET: api/productorder/buyer/{buyerId}
        [HttpGet("buyer/{buyerId:guid}")]
        public async Task<IActionResult> GetOrdersByBuyerId(Guid buyerId)
        {
            var result = await _service.GetOrdersByBuyerIdAsync(buyerId);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }

        // GET: api/productorder/product/{productId}
        [HttpGet("product/{productId:guid}")]
        public async Task<IActionResult> GetOrdersByProductId(Guid productId)
        {
            var result = await _service.GetOrdersByProductIdAsync(productId);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }

        // GET: api/productorder/total-sales/{productId}
        [HttpGet("total-sales/{productId:guid}")]
        public async Task<IActionResult> GetTotalSalesForProduct(Guid productId)
        {
            var result = await _service.GetTotalSalesForProductAsync(productId);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }
    }
}
