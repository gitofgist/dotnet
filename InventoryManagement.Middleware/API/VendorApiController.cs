using Microsoft.AspNetCore.Mvc;
using InventoryManagement.Service.BusinessLayer;
using InventoryManagement.Service.Models;

namespace InventoryManagement.Middleware.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class VendorApiController : ControllerBase
    {
        private readonly VendorManager _vendorManager;

        public VendorApiController(VendorManager vendorManager)
        {
            _vendorManager = vendorManager;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vendor>>> GetVendors()
        {
            try
            {
                var vendors = await _vendorManager.GetAllVendorsAsync();
                return Ok(vendors);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Vendor>> GetVendor(int id)
        {
            try
            {
                var vendor = await _vendorManager.GetVendorByIdAsync(id);
                if (vendor == null)
                    return NotFound();

                return Ok(vendor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Vendor>> CreateVendor(Vendor vendor)
        {
            try
            {
                var createdVendor = await _vendorManager.CreateVendorAsync(vendor);
                return CreatedAtAction(nameof(GetVendor), new { id = createdVendor.Id }, createdVendor);
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
        public async Task<IActionResult> UpdateVendor(int id, Vendor vendor)
        {
            try
            {
                if (id != vendor.Id)
                    return BadRequest();

                var updatedVendor = await _vendorManager.UpdateVendorAsync(vendor);
                return Ok(updatedVendor);
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
        public async Task<IActionResult> DeleteVendor(int id)
        {
            try
            {
                await _vendorManager.DeleteVendorAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
