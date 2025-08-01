using System.ComponentModel.DataAnnotations;
using InventoryManagement.Server.Vendor;
using InventoryManagement.Server.Product;

namespace InventoryManagement.Interface.VendorProduct
{
    public interface VendorProduct
    {
        int Id { get; set; }
        
        [Required]
        int VendorId { get; set; }
        
        [Required]
        int ProductId { get; set; }
        
        decimal Price { get; set; }
        
        [StringLength(50)]
        string VendorProductCode { get; set; }
        
        [StringLength(50)]
        string Unit { get; set; }
        
        int LeadTimeDays { get; set; }
        
        decimal MinOrderQuantity { get; set; }
        
        bool IsPreferredVendor { get; set; }
        
        bool IsActive { get; set; }
        
        DateTime CreatedAt { get; set; }
        
        DateTime? LastUpdated { get; set; }
        
        InventoryManagement.Server.Vendor.Vendor Vendor { get; set; }
        InventoryManagement.Server.Product.Product Product { get; set; }
    }
} 