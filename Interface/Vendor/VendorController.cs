using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryManagement.Data;
using InventoryManagement.Interface.Vendor;

namespace InventoryManagement.Interface.Vendor
{
    public class VendorController : Controller
    {
        private readonly ApplicationDbContext _context;
        
        public VendorController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<IActionResult> Index()
        {
            var vendors = await _context.Vendors
                .Include(v => v.ProductEntries)
                .OrderBy(v => v.Name)
                .ToListAsync();
            return View(vendors);
        }
        
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InventoryManagement.Server.Vendor.Vendor vendor)
        {
            if (ModelState.IsValid)
            {
                _context.Vendors.Add(vendor);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Vendor created successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(vendor);
        }
        
        public async Task<IActionResult> Edit(int id)
        {
            var vendor = await _context.Vendors.FindAsync(id);
            if (vendor == null)
            {
                return NotFound();
            }
            return View(vendor);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, InventoryManagement.Server.Vendor.Vendor vendor)
        {
            if (id != vendor.Id)
            {
                return NotFound();
            }
            
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vendor);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Vendor updated successfully!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VendorExists(vendor.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vendor);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var vendor = await _context.Vendors.FindAsync(id);
            if (vendor != null)
            {
                // Check if vendor has associated product entries
                var hasEntries = await _context.ProductEntries.AnyAsync(p => p.VendorId == id);
                if (hasEntries)
                {
                    TempData["Error"] = "Cannot delete vendor. Vendor has associated product entries.";
                }
                else
                {
                    _context.Vendors.Remove(vendor);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Vendor deleted successfully!";
                }
            }
            
            return RedirectToAction(nameof(Index));
        }
        
        private bool VendorExists(int id)
        {
            return _context.Vendors.Any(e => e.Id == id);
        }
    }
} 