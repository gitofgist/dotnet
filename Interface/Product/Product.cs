using System.ComponentModel.DataAnnotations;
using InventoryManagement.Server.VendorProduct;
using InventoryManagement.Server.Product;

namespace InventoryManagement.Interface.Product
{
    public interface Product
    {
        int Id { get; set; }
        
        [Required]
        [StringLength(200)]
        string Name { get; set; }
        
        [StringLength(500)]
        string Description { get; set; }
        
        [StringLength(50)]
        string Category { get; set; }
        
        [StringLength(100)]
        string Brand { get; set; }
        
        DateTime CreatedAt { get; set; }
        
        ICollection<InventoryManagement.Server.VendorProduct.VendorProduct> VendorProducts { get; set; }
        ICollection<InventoryManagement.Server.Product.ProductEntry> ProductEntries { get; set; }
    }
} 