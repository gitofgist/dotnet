using InventoryManagement.Service.Models;

namespace InventoryManagement.Service.Server.Product
{
    public interface IProductEntry
    {
        Task<IEnumerable<Models.ProductEntry>> GetAllAsync();
        Task<Models.ProductEntry?> GetByIdAsync(int id);
        Task<Models.ProductEntry> CreateAsync(Models.ProductEntry productEntry);
        Task<Models.ProductEntry> UpdateAsync(Models.ProductEntry productEntry);
        Task DeleteAsync(int id);
    }
}
