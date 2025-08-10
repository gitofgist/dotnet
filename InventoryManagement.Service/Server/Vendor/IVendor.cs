using InventoryManagement.Service.Models;

namespace InventoryManagement.Service.Server.Vendor
{
    public interface IVendor
    {
        Task<IEnumerable<Models.Vendor>> GetAllAsync();
        Task<Models.Vendor?> GetByIdAsync(int id);
        Task<Models.Vendor> CreateAsync(Models.Vendor vendor);
        Task<Models.Vendor> UpdateAsync(Models.Vendor vendor);
        Task DeleteAsync(int id);
    }
}
