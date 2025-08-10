using InventoryManagement.Service.Models;
using InventoryManagement.Service.Server.VendorProduct;

namespace InventoryManagement.Service.BusinessLayer
{
    public class VendorProductManager
    {
        private readonly IVendorProduct _vendorProductService;

        public VendorProductManager(IVendorProduct vendorProductService)
        {
            _vendorProductService = vendorProductService;
        }

        public async Task<IEnumerable<VendorProduct>> GetAllVendorProductsAsync()
        {
            return await _vendorProductService.GetAllAsync();
        }

        public async Task<VendorProduct?> GetVendorProductByIdAsync(int id)
        {
            return await _vendorProductService.GetByIdAsync(id);
        }

        public async Task<VendorProduct> CreateVendorProductAsync(VendorProduct vendorProduct)
        {
            // Add business logic validation here
            if (vendorProduct.Price < 0)
                throw new ArgumentException("Price cannot be negative");

            return await _vendorProductService.CreateAsync(vendorProduct);
        }

        public async Task<VendorProduct> UpdateVendorProductAsync(VendorProduct vendorProduct)
        {
            // Add business logic validation here
            if (vendorProduct.Price < 0)
                throw new ArgumentException("Price cannot be negative");

            return await _vendorProductService.UpdateAsync(vendorProduct);
        }

        public async Task DeleteVendorProductAsync(int id)
        {
            await _vendorProductService.DeleteAsync(id);
        }
    }
}
