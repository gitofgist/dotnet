using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryManagement.Service.Data;
using InventoryManagement.Service.Models;
using InventoryManagement.Client.CommonModels;

namespace InventoryManagement.Middleware.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<IActionResult> Index()
        {
            var inventories = await _context.Inventories
                .Include(i => i.ProductEntries)
                .OrderBy(i => i.Name)
                .ToListAsync();
            return View(inventories);
        }
        
        public IActionResult CreateInventory()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateInventory(Inventory inventory)
        {
            if (ModelState.IsValid)
            {
                _context.Inventories.Add(inventory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(inventory);
        }
        
        public async Task<IActionResult> EditInventory(int id)
        {
            var inventory = await _context.Inventories
                .Include(i => i.ProductEntries)
                    .ThenInclude(p => p.Vendor)
                .FirstOrDefaultAsync(i => i.Id == id);
                
            if (inventory == null)
            {
                return NotFound();
            }
            
            ViewBag.Units = Units.AvailableUnits;
            ViewBag.Vendors = await _context.Vendors.OrderBy(v => v.Name).ToListAsync();
            return View(inventory);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddProductEntry(int inventoryId, ProductEntry productEntry)
        {
            productEntry.InventoryId = inventoryId;
            productEntry.CreatedAt = DateTime.Now;
            
            // Remove navigation properties from model validation
            ModelState.Remove("Inventory");
            ModelState.Remove("Vendor");
            
            // Add model validation check
            if (ModelState.IsValid)
            {
                try
                {
                    _context.ProductEntries.Add(productEntry);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Product entry added successfully!";
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Error adding product entry: " + ex.Message;
                }
            }
            else
            {
                // Add validation errors to TempData for debugging
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                TempData["Error"] = "Validation errors: " + string.Join(", ", errors);
            }
            
            return RedirectToAction(nameof(EditInventory), new { id = inventoryId });
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteProductEntry(int id, int inventoryId)
        {
            var productEntry = await _context.ProductEntries.FindAsync(id);
            if (productEntry != null)
            {
                _context.ProductEntries.Remove(productEntry);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Product entry deleted successfully!";
            }
            
            return RedirectToAction(nameof(EditInventory), new { id = inventoryId });
        }
        
        // Fixed: Removed async since no await operations
        [HttpPost]
        public IActionResult SelectInventory(int selectedInventoryId)
        {
            if (selectedInventoryId > 0)
            {
                return RedirectToAction(nameof(EditInventory), new { id = selectedInventoryId });
            }
            return RedirectToAction(nameof(Index));
        }
        
        private bool InventoryExists(int id)
        {
            return _context.Inventories.Any(e => e.Id == id);
        }
    }
} 