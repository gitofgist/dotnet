using Microsoft.EntityFrameworkCore;
using InventoryManagement.Service.Data;
using InventoryManagement.Service.Models;

namespace InventoryManagement.Service.Server.Vendor
{
    public class VendorImpl : IVendor
    {
        private readonly ApplicationDbContext _context;

        public VendorImpl(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Models.Vendor>> GetAllAsync()
        {
            return await _context.Vendors
                .Include(v => v.ProductEntries)
                .Include(v => v.VendorProducts)
                .ToListAsync();
        }

        public async Task<Models.Vendor?> GetByIdAsync(int id)
        {
            return await _context.Vendors
                .Include(v => v.ProductEntries)
                .Include(v => v.VendorProducts)
                .FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task<Models.Vendor> CreateAsync(Models.Vendor vendor)
        {
            _context.Vendors.Add(vendor);
            await _context.SaveChangesAsync();
            return vendor;
        }

        public async Task<Models.Vendor> UpdateAsync(Models.Vendor vendor)
        {
            _context.Vendors.Update(vendor);
            await _context.SaveChangesAsync();
            return vendor;
        }

        public async Task DeleteAsync(int id)
        {
            var vendor = await GetByIdAsync(id);
            if (vendor != null)
            {
                _context.Vendors.Remove(vendor);
                await _context.SaveChangesAsync();
            }
        }
    }
}
