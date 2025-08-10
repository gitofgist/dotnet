using Microsoft.EntityFrameworkCore;
using InventoryManagement.Service.Data;
using InventoryManagement.Service.Models;

namespace InventoryManagement.Service.Server.Product
{
    public class ProductImpl : IProduct
    {
        private readonly ApplicationDbContext _context;

        public ProductImpl(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Models.Product>> GetAllAsync()
        {
            return await _context.Products
                .Include(p => p.VendorProducts)
                .Include(p => p.ProductEntries)
                .ToListAsync();
        }

        public async Task<Models.Product?> GetByIdAsync(int id)
        {
            return await _context.Products
                .Include(p => p.VendorProducts)
                .Include(p => p.ProductEntries)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Models.Product> CreateAsync(Models.Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Models.Product> UpdateAsync(Models.Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task DeleteAsync(int id)
        {
            var product = await GetByIdAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }
    }
}
