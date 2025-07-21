using EcommerceTask.Application.DTOs;
using EcommerceTask.Application.DTOs.Cart;
using EcommerceTask.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceTask.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly UserSessionInfo _userSessionInfo;
        public CartController(ICartService cartService, UserSessionInfo userSessionInfo)
        {
            _cartService = cartService;
            _userSessionInfo = userSessionInfo;
        }

        [HttpPost("add-to-cart")]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartDto addToCartDto)
        {
            var response = await _cartService.AddToCartAsync(addToCartDto, _userSessionInfo.UserId);
            return Ok(response);
        }

        [HttpGet("get-cart-items")]
        public async Task<IActionResult> GetCartItems()
        {
            var cartItems = await _cartService.GetCartItemsAsync(_userSessionInfo.UserId);
            return Ok(cartItems);
        }

        [HttpDelete("remove-from-cart/{productId}")]
        public async Task<IActionResult> RemoveFromCart(Guid productId)
        {
            var response = await _cartService.RemoveFromCartAsync(productId);
            return Ok(response);
        }

        [HttpDelete("clear-cart/{productId}")]
        public async Task<IActionResult> ClearCart()
        {
            var response = await _cartService.ClearCartAsync(_userSessionInfo.UserId);
            return Ok(response);
        }
    }
}
