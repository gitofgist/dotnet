using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Service.Models
{
    public class Product
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
        
        public virtual ICollection<VendorProduct> VendorProducts { get; set; } = new List<VendorProduct>();
        public virtual ICollection<ProductEntry> ProductEntries { get; set; } = new List<ProductEntry>();
    }
}
