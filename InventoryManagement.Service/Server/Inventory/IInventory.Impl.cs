using Microsoft.EntityFrameworkCore;
using InventoryManagement.Service.Data;
using InventoryManagement.Service.Models;

namespace InventoryManagement.Service.Server.Inventory
{
    public class InventoryImpl : IInventory
    {
        private readonly ApplicationDbContext _context;

        public InventoryImpl(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Models.Inventory>> GetAllAsync()
        {
            return await _context.Inventories
                .Include(i => i.ProductEntries)
                .ToListAsync();
        }

        public async Task<Models.Inventory?> GetByIdAsync(int id)
        {
            return await _context.Inventories
                .Include(i => i.ProductEntries)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Models.Inventory> CreateAsync(Models.Inventory inventory)
        {
            _context.Inventories.Add(inventory);
            await _context.SaveChangesAsync();
            return inventory;
        }

        public async Task<Models.Inventory> UpdateAsync(Models.Inventory inventory)
        {
            _context.Inventories.Update(inventory);
            await _context.SaveChangesAsync();
            return inventory;
        }

        public async Task DeleteAsync(int id)
        {
            var inventory = await GetByIdAsync(id);
            if (inventory != null)
            {
                _context.Inventories.Remove(inventory);
                await _context.SaveChangesAsync();
            }
        }
    }
}