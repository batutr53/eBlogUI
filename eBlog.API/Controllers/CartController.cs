﻿using eBlog.Application.DTOs;
using eBlog.Application.Extensions;
using eBlog.Application.Interfaces;
using eBlog.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace eBlog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _service;
        public CartController(ICartService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userId = User.GetUserId(); // extension method varsa
            var result = await _service.GetCartItemsAsync(userId);
            return Ok(result);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(AddToCartDto dto)
        {
            var userId = User.GetUserId();
            var result = await _service.AddToCartAsync(userId, dto);
            return Ok(result);
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> Remove(Guid productId)
        {
            var userId = User.GetUserId();
            var result = await _service.RemoveFromCartAsync(userId, productId);
            return Ok(result);
        }

        [HttpDelete("clear")]
        public async Task<IActionResult> Clear()
        {
            var userId = User.GetUserId();
            var result = await _service.ClearCartAsync(userId);
            return Ok(result);
        }

        [HttpGet("all")]
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

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CartDto dto)
        {
            var result = await _service.AddAsync(dto);
            if (!result.Success)
                return BadRequest(result);
            return CreatedAtAction(nameof(GetById), new { id = ((dynamic)result.Data).Id }, result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CartDto dto)
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

        // GET: api/cart/user/{userId}
        [HttpGet("user/{userId:guid}")]
        public async Task<IActionResult> GetCartByUserId(Guid userId)
        {
            var result = await _service.GetCartByUserIdAsync(userId);
            if (!result.Success)
                return NotFound(result);
            return Ok(result);
        }

        // GET: api/cart/total-price/{cartId}
        [HttpGet("total-price/{cartId:guid}")]
        public async Task<IActionResult> GetTotalCartPrice(Guid cartId)
        {
            var result = await _service.GetTotalCartPriceAsync(cartId);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }
    }
}
