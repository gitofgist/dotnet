using InventoryManagement.Service.Models;

namespace InventoryManagement.Service.Server.VendorProduct
{
    public interface IVendorProduct
    {
        Task<IEnumerable<Models.VendorProduct>> GetAllAsync();
        Task<Models.VendorProduct?> GetByIdAsync(int id);
        Task<Models.VendorProduct> CreateAsync(Models.VendorProduct vendorProduct);
        Task<Models.VendorProduct> UpdateAsync(Models.VendorProduct vendorProduct);
        Task DeleteAsync(int id);
    }
}
