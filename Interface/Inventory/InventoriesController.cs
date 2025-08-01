using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryManagement.Data;
using InventoryManagement.Interface.Inventory;

namespace InventoryManagement.Interface.Inventory
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        
        public InventoriesController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        // GET: api/inventories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventoryManagement.Interface.Inventory.Inventory>>> GetInventories()
        {
            return await _context.Inventories
                .Include(i => i.ProductEntries)
                .ToListAsync();
        }
        
        // GET: api/inventories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InventoryManagement.Interface.Inventory.Inventory>> GetInventory(int id)
        {
            var inventory = await _context.Inventories
                .Include(i => i.ProductEntries)
                .FirstOrDefaultAsync(i => i.Id == id);
                
            if (inventory == null)
            {
                return NotFound();
            }
            
            return inventory;
        }
        
        // POST: api/inventories
        [HttpPost]
        public async Task<ActionResult<InventoryManagement.Interface.Inventory.Inventory>> PostInventory(InventoryManagement.Interface.Inventory.Inventory inventory)
        {
            _context.Inventories.Add((InventoryManagement.Server.Inventory.Inventory)inventory);
            await _context.SaveChangesAsync();
            
            return CreatedAtAction(nameof(GetInventory), new { id = inventory.Id }, inventory);
        }
        
        // PUT: api/inventories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInventory(int id, InventoryManagement.Interface.Inventory.Inventory inventory)
        {
            if (id != inventory.Id)
            {
                return BadRequest();
            }
            
            _context.Entry((InventoryManagement.Server.Inventory.Inventory)inventory).State = EntityState.Modified;
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InventoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            
            return NoContent();
        }
        
        // DELETE: api/inventories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInventory(int id)
        {
            var inventory = await _context.Inventories.FindAsync(id);
            if (inventory == null)
            {
                return NotFound();
            }
            
            _context.Inventories.Remove(inventory);
            await _context.SaveChangesAsync();
            
            return NoContent();
        }
        
        private bool InventoryExists(int id)
        {
            return _context.Inventories.Any(e => e.Id == id);
        }
    }
} 