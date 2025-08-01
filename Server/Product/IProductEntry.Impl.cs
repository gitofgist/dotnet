using System.ComponentModel.DataAnnotations;
using InventoryManagement.Server.Vendor;
using InventoryManagement.Server.Product;
using InventoryManagement.Server.VendorProduct;
using InventoryManagement.Interface.ProductEntry;

namespace InventoryManagement.Server.Product
{
    public class ProductEntry : InventoryManagement.Interface.ProductEntry.ProductEntry
    {
        public int Id { get; set; }
        
        [Required]
        public int InventoryId { get; set; }
        
        // Keep the old VendorId for backward compatibility during migration
        [Required]
        public int VendorId { get; set; }
        
        // NEW PROPERTIES - Add these
        public int? ProductId { get; set; }  // Nullable during migration
        public int? VendorProductId { get; set; }  // Nullable during migration
        
        [Required]
        [StringLength(50)]
        public string ProjectId { get; set; } = string.Empty;
        
        [Required]
        [StringLength(200)]
        public string ProductName { get; set; } = string.Empty;  // Keep this during migration
        
        [Required]
        [StringLength(50)]
        public string Unit { get; set; } = string.Empty;
        
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
        public decimal Quantity { get; set; }
        
        // NEW PROPERTY
        public decimal UnitPrice { get; set; } = 0.00m;
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        // Navigation properties - OLD (keep for now)
        public virtual InventoryManagement.Server.Inventory.Inventory? Inventory { get; set; }
        public virtual InventoryManagement.Server.Vendor.Vendor? Vendor { get; set; }
        
        // Navigation properties - NEW (add these)
        public virtual InventoryManagement.Server.Product.Product? Product { get; set; }
        public virtual InventoryManagement.Server.VendorProduct.VendorProduct? VendorProduct { get; set; }
    }
}