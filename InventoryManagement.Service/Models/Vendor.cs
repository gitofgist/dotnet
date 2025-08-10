using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Service.Models
{
    public class Vendor
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
        public virtual ICollection<ProductEntry> ProductEntries { get; set; } = new List<ProductEntry>();
        
        // NEW - add this property
        public virtual ICollection<VendorProduct> VendorProducts { get; set; } = new List<VendorProduct>();
    }
}
