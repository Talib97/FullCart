using FullCart.Application.Model;
using FullCart.Application.Services;
using FullCart.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Fullcart.Controllers
{
    [Route("api/order")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<List<OrderDto>>> Get([Required][FromRoute] Guid userId)
        {
            return Ok(await _orderService.GetOrder(userId));
        }

        [HttpPost]
        public async Task<ActionResult<List<OrderDto>>> Post([Required][FromBody] CreateOrderDto order )
        {
            await _orderService.CreateOrder(order);
            return Ok();
        }

        [HttpPatch("{orderId}/cancel")]
        public async Task<ActionResult> CancelOrder([Required][FromRoute] Guid orderId,CreateOrderDto order)
        {
            await _orderService.CancelOrder(orderId,order);
            return Ok();
        }
    }
}
