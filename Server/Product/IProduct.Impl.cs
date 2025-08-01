using System.ComponentModel.DataAnnotations;
using InventoryManagement.Server.VendorProduct;
using InventoryManagement.Server.Inventory;
using InventoryManagement.Interface.Product;

namespace InventoryManagement.Server.Product
{
    public class Product : InventoryManagement.Interface.Product.Product
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;
        
        [StringLength(500)]
        public string Description { get; set; } = string.Empty;
        
        [StringLength(50)]
        public string Category { get; set; } = string.Empty;
        
        [StringLength(100)]
        public string Brand { get; set; } = string.Empty;
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        public virtual ICollection<InventoryManagement.Server.VendorProduct.VendorProduct> VendorProducts { get; set; } = new List<InventoryManagement.Server.VendorProduct.VendorProduct>();
        public virtual ICollection<InventoryManagement.Server.Product.ProductEntry> ProductEntries { get; set; } = new List<InventoryManagement.Server.Product.ProductEntry>();
    }
}
