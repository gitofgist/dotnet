using Microsoft.AspNetCore.Mvc;
using InventoryManagement.Service.BusinessLayer;
using InventoryManagement.Service.Models;

namespace InventoryManagement.Middleware.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class VendorProductApiController : ControllerBase
    {
        private readonly VendorProductManager _vendorProductManager;

        public VendorProductApiController(VendorProductManager vendorProductManager)
        {
            _vendorProductManager = vendorProductManager;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VendorProduct>>> GetVendorProducts()
        {
            try
            {
                var vendorProducts = await _vendorProductManager.GetAllVendorProductsAsync();
                return Ok(vendorProducts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VendorProduct>> GetVendorProduct(int id)
        {
            try
            {
                var vendorProduct = await _vendorProductManager.GetVendorProductByIdAsync(id);
                if (vendorProduct == null)
                    return NotFound();

                return Ok(vendorProduct);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<VendorProduct>> CreateVendorProduct(VendorProduct vendorProduct)
        {
            try
            {
                var createdVendorProduct = await _vendorProductManager.CreateVendorProductAsync(vendorProduct);
                return CreatedAtAction(nameof(GetVendorProduct), new { id = createdVendorProduct.Id }, createdVendorProduct);
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
        public async Task<IActionResult> UpdateVendorProduct(int id, VendorProduct vendorProduct)
        {
            try
            {
                if (id != vendorProduct.Id)
                    return BadRequest();

                var updatedVendorProduct = await _vendorProductManager.UpdateVendorProductAsync(vendorProduct);
                return Ok(updatedVendorProduct);
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
        public async Task<IActionResult> DeleteVendorProduct(int id)
        {
            try
            {
                await _vendorProductManager.DeleteVendorProductAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
