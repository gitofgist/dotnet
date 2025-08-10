using Microsoft.AspNetCore.Mvc;
using InventoryManagement.Service.BusinessLayer;
using InventoryManagement.Service.Models;

namespace InventoryManagement.Middleware.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryApiController : ControllerBase
    {
        private readonly InventoryManager _inventoryManager;

        public InventoryApiController(InventoryManager inventoryManager)
        {
            _inventoryManager = inventoryManager;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Inventory>>> GetInventories()
        {
            try
            {
                var inventories = await _inventoryManager.GetAllInventoriesAsync();
                return Ok(inventories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Inventory>> GetInventory(int id)
        {
            try
            {
                var inventory = await _inventoryManager.GetInventoryByIdAsync(id);
                if (inventory == null)
                    return NotFound();

                return Ok(inventory);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Inventory>> CreateInventory(Inventory inventory)
        {
            try
            {
                var createdInventory = await _inventoryManager.CreateInventoryAsync(inventory);
                return CreatedAtAction(nameof(GetInventory), new { id = createdInventory.Id }, createdInventory);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInventory(int id, Inventory inventory)
        {
            try
            {
                if (id != inventory.Id)
                    return BadRequest();

                var updatedInventory = await _inventoryManager.UpdateInventoryAsync(inventory);
                return Ok(updatedInventory);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInventory(int id)
        {
            try
            {
                await _inventoryManager.DeleteInventoryAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
