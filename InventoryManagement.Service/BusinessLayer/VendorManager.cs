using InventoryManagement.Service.Models;
using InventoryManagement.Service.Server.Vendor;

namespace InventoryManagement.Service.BusinessLayer
{
    public class VendorManager
    {
        private readonly IVendor _vendorService;

        public VendorManager(IVendor vendorService)
        {
            _vendorService = vendorService;
        }

        public async Task<IEnumerable<Vendor>> GetAllVendorsAsync()
        {
            return await _vendorService.GetAllAsync();
        }

        public async Task<Vendor?> GetVendorByIdAsync(int id)
        {
            return await _vendorService.GetByIdAsync(id);
        }

        public async Task<Vendor> CreateVendorAsync(Vendor vendor)
        {
            // Add business logic validation here
            if (string.IsNullOrWhiteSpace(vendor.Name))
                throw new ArgumentException("Vendor name cannot be empty");

            if (string.IsNullOrWhiteSpace(vendor.Email))
                throw new ArgumentException("Vendor email cannot be empty");

            return await _vendorService.CreateAsync(vendor);
        }

        public async Task<Vendor> UpdateVendorAsync(Vendor vendor)
        {
            // Add business logic validation here
            if (string.IsNullOrWhiteSpace(vendor.Name))
                throw new ArgumentException("Vendor name cannot be empty");

            if (string.IsNullOrWhiteSpace(vendor.Email))
                throw new ArgumentException("Vendor email cannot be empty");

            return await _vendorService.UpdateAsync(vendor);
        }

        public async Task DeleteVendorAsync(int id)
        {
            await _vendorService.DeleteAsync(id);
        }
    }
}
