using FullCart.Application.Model;
using FullCart.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Fullcart.Controllers
{
    [Route("api/inventory")]
    [ApiController]
    [Authorize]
    public class InventoryController : ControllerBase
    {
        private readonly InventoryService _inventoryService;

        public InventoryController(InventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }


        [HttpGet("product")]
        public async Task<ActionResult<List<InventoryDto>>> GetInventoryProduct([FromQuery] Guid? categoryId = null,
                                                                                [FromQuery] string sortOrder = null,
                                                                                [FromQuery] string searchTerm = null)
        {
            return Ok(await _inventoryService.GetInventoryProductAsync(categoryId, sortOrder, searchTerm));
        }

        [HttpGet("status")]
        public async Task<ActionResult<List<InventoryStatusDto>>> GetInventoryStatus()
        {
            return Ok(await _inventoryService.GetInventoryStatusAsync());
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody][Required] InventoryDto inventory)
        {
            await _inventoryService.SaveInventoryProduct(inventory);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody][Required] InventoryDto inventory)
        {
            ArgumentException.ThrowIfNullOrEmpty(nameof(inventory.Id));
            await _inventoryService.updateInventoryProduct(inventory);
            return Ok();
        }

        [HttpDelete("{inventoryId}")]
        public async Task<ActionResult> Delete([FromRoute][Required] Guid inventoryId)
        {
            ArgumentException.ThrowIfNullOrEmpty(nameof(inventoryId));
            await _inventoryService.DeleteInventoryProduct(inventoryId);
            return Ok();
        }
    }
}
