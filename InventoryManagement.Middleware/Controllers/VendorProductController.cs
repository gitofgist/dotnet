using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryManagement.Service.Data;
using InventoryManagement.Service.Models;
using InventoryManagement.Client.CommonModels;

namespace InventoryManagement.Middleware.Controllers
{
    public class VendorProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        
        public VendorProductController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        // GET: VendorProduct
        public async Task<IActionResult> Index()
        {
            var vendorProducts = await _context.VendorProducts
                .Include(vp => vp.Vendor)
                .Include(vp => vp.Product)
                .OrderBy(vp => vp.Vendor.Name)
                .ThenBy(vp => vp.Product.Name)
                .ToListAsync();
            return View(vendorProducts);
        }
        
        // GET: VendorProduct/Create
        public async Task<IActionResult> Create(int? vendorId, int? productId)
        {
            ViewBag.Vendors = await _context.Vendors.OrderBy(v => v.Name).ToListAsync();
            ViewBag.Products = await _context.Products.OrderBy(p => p.Name).ToListAsync();
            ViewBag.Units = Units.AvailableUnits;
            
            var vendorProduct = new VendorProduct();
            if (vendorId.HasValue) vendorProduct.VendorId = vendorId.Value;
            if (productId.HasValue) vendorProduct.ProductId = productId.Value;
            
            return View(vendorProduct);
        }
        
        // POST: VendorProduct/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VendorProduct vendorProduct)
        {
            Console.WriteLine($"[DEBUG] POST Create called. VendorId: {vendorProduct.VendorId}, ProductId: {vendorProduct.ProductId}, Price: {vendorProduct.Price}, Unit: {vendorProduct.Unit}");
            Console.WriteLine($"[DEBUG] ModelState.IsValid: {ModelState.IsValid}");
            if (!ModelState.IsValid)
            {
                foreach (var key in ModelState.Keys)
                {
                    var modelStateEntry = ModelState[key];
                    if (modelStateEntry?.Errors != null)
                    {
                        foreach (var error in modelStateEntry.Errors)
                        {
                            Console.WriteLine($"[DEBUG] ModelState error for {key}: {error.ErrorMessage}");
                        }
                    }
                }
            }
            // Check if this vendor-product combination already exists
            var exists = await _context.VendorProducts
                .AnyAsync(vp => vp.VendorId == vendorProduct.VendorId && vp.ProductId == vendorProduct.ProductId);
                
            if (exists)
            {
                ModelState.AddModelError("", "This vendor already supplies this product.");
                Console.WriteLine("[DEBUG] Duplicate vendor-product relationship detected.");
            }
            
            if (ModelState.IsValid)
            {
                vendorProduct.CreatedAt = DateTime.Now;
                _context.VendorProducts.Add(vendorProduct);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Vendor-Product relationship created successfully!";
                Console.WriteLine("[DEBUG] Vendor-Product relationship created successfully.");
                return RedirectToAction(nameof(Index));
            }
            
            ViewBag.Vendors = await _context.Vendors.OrderBy(v => v.Name).ToListAsync();
            ViewBag.Products = await _context.Products.OrderBy(p => p.Name).ToListAsync();
            ViewBag.Units = Units.AvailableUnits;
            return View(vendorProduct);
        }
        
        // GET: VendorProduct/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var vendorProduct = await _context.VendorProducts
                .Include(vp => vp.Vendor)
                .Include(vp => vp.Product)
                .FirstOrDefaultAsync(vp => vp.Id == id);
                
            if (vendorProduct == null)
            {
                return NotFound();
            }
            
            ViewBag.Vendors = await _context.Vendors.OrderBy(v => v.Name).ToListAsync();
            ViewBag.Products = await _context.Products.OrderBy(p => p.Name).ToListAsync();
            ViewBag.Units = Units.AvailableUnits;
            return View(vendorProduct);
        }
        
        // POST: VendorProduct/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, VendorProduct vendorProduct)
        {
            if (id != vendorProduct.Id)
            {
                return NotFound();
            }
            
            if (ModelState.IsValid)
            {
                try
                {
                    vendorProduct.LastUpdated = DateTime.Now;
                    _context.Update(vendorProduct);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Vendor-Product relationship updated successfully!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VendorProductExists(vendorProduct.Id))
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
            
            ViewBag.Vendors = await _context.Vendors.OrderBy(v => v.Name).ToListAsync();
            ViewBag.Products = await _context.Products.OrderBy(p => p.Name).ToListAsync();
            ViewBag.Units = Units.AvailableUnits;
            return View(vendorProduct);
        }
        
        // POST: VendorProduct/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var vendorProduct = await _context.VendorProducts.FindAsync(id);
            if (vendorProduct != null)
            {
                // Check if this vendor-product is used in any inventory entries
                var inUse = await _context.ProductEntries.AnyAsync(pe => pe.VendorProductId == id);
                if (inUse)
                {
                    TempData["Error"] = "Cannot delete this vendor-product relationship. It's being used in inventory entries.";
                }
                else
                {
                    _context.VendorProducts.Remove(vendorProduct);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Vendor-Product relationship deleted successfully!";
                }
            }
            
            return RedirectToAction(nameof(Index));
        }
        
        // POST: Add vendor to product (AJAX)
        [HttpPost]
        public async Task<IActionResult> AddVendorToProduct(int productId, int vendorId, decimal price, string unit, int leadTimeDays)
        {
            try
            {
                var exists = await _context.VendorProducts
                    .AnyAsync(vp => vp.VendorId == vendorId && vp.ProductId == productId);
                    
                if (exists)
                {
                    return Json(new { success = false, message = "This vendor already supplies this product." });
                }
                
                var vendorProduct = new VendorProduct
                {
                    VendorId = vendorId,
                    ProductId = productId,
                    Price = price,
                    Unit = unit,
                    LeadTimeDays = leadTimeDays,
                    MinOrderQuantity = 1,
                    IsPreferredVendor = false,
                    IsActive = true,
                    CreatedAt = DateTime.Now
                };
                
                _context.VendorProducts.Add(vendorProduct);
                await _context.SaveChangesAsync();
                
                return Json(new { success = true, message = "Vendor added to product successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error: " + ex.Message });
            }
        }
        
        private bool VendorProductExists(int id)
        {
            return _context.VendorProducts.Any(e => e.Id == id);
        }
    }
} 