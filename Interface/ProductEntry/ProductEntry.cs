using System.ComponentModel.DataAnnotations;
using InventoryManagement.Server.Vendor;
using InventoryManagement.Server.Product;
using InventoryManagement.Server.VendorProduct;

namespace InventoryManagement.Interface.ProductEntry
{
    public interface ProductEntry
    {
        int Id { get; set; }
        
        [Required]
        int InventoryId { get; set; }
        
        [Required]
        int VendorId { get; set; }
        
        int? ProductId { get; set; }
        int? VendorProductId { get; set; }
        
        [Required]
        [StringLength(50)]
        string ProjectId { get; set; }
        
        [Required]
        [StringLength(200)]
        string ProductName { get; set; }
        
        [Required]
        [StringLength(50)]
        string Unit { get; set; }
        
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
        decimal Quantity { get; set; }
        
        decimal UnitPrice { get; set; }
        
        DateTime CreatedAt { get; set; }
        
        InventoryManagement.Server.Inventory.Inventory? Inventory { get; set; }
        InventoryManagement.Server.Vendor.Vendor? Vendor { get; set; }
        InventoryManagement.Server.Product.Product? Product { get; set; }
        InventoryManagement.Server.VendorProduct.VendorProduct? VendorProduct { get; set; }
    }
} 