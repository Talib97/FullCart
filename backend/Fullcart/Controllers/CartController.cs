using FullCart.Application.Model;
using FullCart.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Fullcart.Controllers
{
    [Route("api/cart")]
    [ApiController]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly CartService _cartService;

        public CartController(CartService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost]
        public async Task<ActionResult> Post([Required][FromBody] CartItemCreateDto cartItem)
        {
            await _cartService.AddItemToCartAsync(cartItem);
            return Ok();
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<List<CartDto>>> Get([Required][FromRoute] Guid userId)
        {
            return Ok(await _cartService.GetCartItemAsync(userId));
        }

        [HttpGet("{userId}/count")]
        public async Task<ActionResult<int>> GetCartItemCount(Guid userId)
        {
            return Ok(await _cartService.GetCartItemCountAsync(userId));
        }

        [HttpPatch("{cartId}/update-quantity")]
        public async Task<ActionResult<UpdateItemQuantityResponse>> UpdateCartItemQuantity([Required][FromBody] CartItemCreateDto cartItem, [Required][FromRoute] Guid cartId)
        {
            return Ok(await _cartService.UpdateCartItemQuantity(cartItem, cartId));
        }

        [HttpDelete("{userId}/remove/{cartId}")]
        public async Task<ActionResult<List<CartDto>>> RemoveCartItem([Required][FromRoute] Guid cartId, [Required][FromRoute] Guid userId)
        {
            return Ok(await _cartService.RemoveCartItemAsync(cartId, userId));
        }
    }
}
