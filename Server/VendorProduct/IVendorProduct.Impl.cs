using System.ComponentModel.DataAnnotations;
using InventoryManagement.Server.Vendor;
using InventoryManagement.Server.Product;
using InventoryManagement.Interface.VendorProduct;

namespace InventoryManagement.Server.VendorProduct
{
    public class VendorProduct : InventoryManagement.Interface.VendorProduct.VendorProduct
    {
        public int Id { get; set; }
        
        [Required]
        public int VendorId { get; set; }
        
        [Required]
        public int ProductId { get; set; }
        
        public decimal Price { get; set; } = 0.00m;
        
        [StringLength(50)]
        public string VendorProductCode { get; set; } = string.Empty;
        
        [StringLength(50)]
        public string Unit { get; set; } = string.Empty;
        
        public int LeadTimeDays { get; set; } = 7;
        
        public decimal MinOrderQuantity { get; set; } = 1.00m;
        
        public bool IsPreferredVendor { get; set; } = false;
        
        public bool IsActive { get; set; } = true;
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        public DateTime? LastUpdated { get; set; }
        
        public virtual InventoryManagement.Server.Vendor.Vendor Vendor { get; set; } = null!;
        public virtual InventoryManagement.Server.Product.Product Product { get; set; } = null!;
    }
}
