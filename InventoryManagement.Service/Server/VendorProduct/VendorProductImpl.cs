using InventoryManagement.Service.Models;

namespace InventoryManagement.Service.Server.VendorProduct
{
    public class VendorProductImpl : IVendorProduct
    {
        public async Task<IEnumerable<Models.VendorProduct>> GetAllAsync()
        {
            // Implementation will be added here
            throw new NotImplementedException();
        }

        public async Task<Models.VendorProduct?> GetByIdAsync(int id)
        {
            // Implementation will be added here
            throw new NotImplementedException();
        }

        public async Task<Models.VendorProduct> CreateAsync(Models.VendorProduct vendorProduct)
        {
            // Implementation will be added here
            throw new NotImplementedException();
        }

        public async Task<Models.VendorProduct> UpdateAsync(Models.VendorProduct vendorProduct)
        {
            // Implementation will be added here
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(int id)
        {
            // Implementation will be added here
            throw new NotImplementedException();
        }
    }
}
