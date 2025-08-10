using Microsoft.EntityFrameworkCore;
using InventoryManagement.Service.Data;
using InventoryManagement.Service.Models;

namespace InventoryManagement.Service.Server.Product
{
    public class ProductEntryImpl : IProductEntry
    {
        private readonly ApplicationDbContext _context;

        public ProductEntryImpl(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Models.ProductEntry>> GetAllAsync()
        {
            return await _context.ProductEntries
                .Include(pe => pe.Inventory)
                .Include(pe => pe.Vendor)
                .Include(pe => pe.Product)
                .Include(pe => pe.VendorProduct)
                .ToListAsync();
        }

        public async Task<Models.ProductEntry?> GetByIdAsync(int id)
        {
            return await _context.ProductEntries
                .Include(pe => pe.Inventory)
                .Include(pe => pe.Vendor)
                .Include(pe => pe.Product)
                .Include(pe => pe.VendorProduct)
                .FirstOrDefaultAsync(pe => pe.Id == id);
        }

        public async Task<Models.ProductEntry> CreateAsync(Models.ProductEntry productEntry)
        {
            _context.ProductEntries.Add(productEntry);
            await _context.SaveChangesAsync();
            return productEntry;
        }

        public async Task<Models.ProductEntry> UpdateAsync(Models.ProductEntry productEntry)
        {
            _context.ProductEntries.Update(productEntry);
            await _context.SaveChangesAsync();
            return productEntry;
        }

        public async Task DeleteAsync(int id)
        {
            var productEntry = await GetByIdAsync(id);
            if (productEntry != null)
            {
                _context.ProductEntries.Remove(productEntry);
                await _context.SaveChangesAsync();
            }
        }
    }
}