using System.ComponentModel.DataAnnotations;
using InventoryManagement.Server.Inventory;
using InventoryManagement.Server.VendorProduct;
using InventoryManagement.Interface.Vendor;

namespace InventoryManagement.Server.Vendor
{
    public class Vendor : InventoryManagement.Interface.Vendor.Vendor
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [Required]
        [StringLength(200)]
        public string Address { get; set; } = string.Empty;
        
        [Required]
        [StringLength(20)]
        public string Phone { get; set; } = string.Empty;
        
        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        // EXISTING - keep this
        public virtual ICollection<InventoryManagement.Server.Product.ProductEntry> ProductEntries { get; set; } = new List<InventoryManagement.Server.Product.ProductEntry>();
        
        // NEW - add this property
        public virtual ICollection<InventoryManagement.Server.VendorProduct.VendorProduct> VendorProducts { get; set; } = new List<InventoryManagement.Server.VendorProduct.VendorProduct>();
    }
}
