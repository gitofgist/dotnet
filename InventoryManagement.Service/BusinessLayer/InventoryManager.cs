using InventoryManagement.Service.Models;
using InventoryManagement.Service.Server.Inventory;

namespace InventoryManagement.Service.BusinessLayer
{
    public class InventoryManager
    {
        private readonly IInventory _inventoryService;

        public InventoryManager(IInventory inventoryService)
        {
            _inventoryService = inventoryService;
        }

        public async Task<IEnumerable<Inventory>> GetAllInventoriesAsync()
        {
            return await _inventoryService.GetAllAsync();
        }

        public async Task<Inventory?> GetInventoryByIdAsync(int id)
        {
            return await _inventoryService.GetByIdAsync(id);
        }

        public async Task<Inventory> CreateInventoryAsync(Inventory inventory)
        {
            // Add business logic validation here
            if (string.IsNullOrWhiteSpace(inventory.Name))
                throw new ArgumentException("Inventory name cannot be empty");

            return await _inventoryService.CreateAsync(inventory);
        }

        public async Task<Inventory> UpdateInventoryAsync(Inventory inventory)
        {
            // Add business logic validation here
            if (string.IsNullOrWhiteSpace(inventory.Name))
                throw new ArgumentException("Inventory name cannot be empty");

            return await _inventoryService.UpdateAsync(inventory);
        }

        public async Task DeleteInventoryAsync(int id)
        {
            await _inventoryService.DeleteAsync(id);
        }
    }
}
