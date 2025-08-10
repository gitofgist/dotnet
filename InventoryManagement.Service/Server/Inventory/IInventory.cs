using InventoryManagement.Service.Models;

namespace InventoryManagement.Service.Server.Inventory
{
    public interface IInventory
    {
        Task<IEnumerable<Models.Inventory>> GetAllAsync();
        Task<Models.Inventory?> GetByIdAsync(int id);
        Task<Models.Inventory> CreateAsync(Models.Inventory inventory);
        Task<Models.Inventory> UpdateAsync(Models.Inventory inventory);
        Task DeleteAsync(int id);
    }
}
