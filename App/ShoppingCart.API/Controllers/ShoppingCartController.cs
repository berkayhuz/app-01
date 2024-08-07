using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.API.DTOs;
using ShoppingCart.API.Services.Abstractions;
using System.Security.Claims;

namespace ShoppingCart.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetCart()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cart = await _shoppingCartService.GetCartByUserAsync(userId);
            return Ok(cart);
        }

        [HttpGet("guest")]
        public async Task<IActionResult> GetGuestCart()
        {
            var ip = HttpContext.Connection.RemoteIpAddress?.ToString();
            var cart = await _shoppingCartService.GetCartByIpAsync(ip);
            return Ok(cart);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddToCart([FromBody] CartItemDto cartItemDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _shoppingCartService.AddToCartAsync(userId, null, cartItemDto);
            return Ok();
        }

        [HttpPost("guest")]
        public async Task<IActionResult> AddToGuestCart([FromBody] CartItemDto cartItemDto)
        {
            var ip = HttpContext.Connection.RemoteIpAddress?.ToString();
            await _shoppingCartService.AddToCartAsync(null, ip, cartItemDto);
            return Ok();
        }

        [HttpGet("total")]
        [Authorize]
        public async Task<IActionResult> GetTotalPrice()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var totalPrice = await _shoppingCartService.GetCartTotalPriceAsync(userId, null);
            return Ok(new { TotalPrice = totalPrice });
        }

        [HttpGet("guest/total")]
        public async Task<IActionResult> GetGuestTotalPrice()
        {
            var ip = HttpContext.Connection.RemoteIpAddress?.ToString();
            var totalPrice = await _shoppingCartService.GetCartTotalPriceAsync(null, ip);
            return Ok(new { TotalPrice = totalPrice });
        }

    }

}
