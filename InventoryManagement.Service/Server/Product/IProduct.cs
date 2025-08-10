using InventoryManagement.Service.Models;

namespace InventoryManagement.Service.Server.Product
{
    public interface IProduct
    {
        Task<IEnumerable<Models.Product>> GetAllAsync();
        Task<Models.Product?> GetByIdAsync(int id);
        Task<Models.Product> CreateAsync(Models.Product product);
        Task<Models.Product> UpdateAsync(Models.Product product);
        Task DeleteAsync(int id);
    }
}
