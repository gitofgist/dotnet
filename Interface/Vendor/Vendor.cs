using System.ComponentModel.DataAnnotations;
using InventoryManagement.Server.Inventory;
using InventoryManagement.Server.VendorProduct;

namespace InventoryManagement.Interface.Vendor
{
    public interface Vendor
    {
        int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        string Name { get; set; }
        
        [Required]
        [StringLength(200)]
        string Address { get; set; }
        
        [Required]
        [StringLength(20)]
        string Phone { get; set; }
        
        [Required]
        [EmailAddress]
        [StringLength(100)]
        string Email { get; set; }
        
        DateTime CreatedAt { get; set; }
        
        ICollection<InventoryManagement.Server.Product.ProductEntry> ProductEntries { get; set; }
        ICollection<InventoryManagement.Server.VendorProduct.VendorProduct> VendorProducts { get; set; }
    }
} 