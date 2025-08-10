using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Service.Models
{
    public class Inventory
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [StringLength(500)]
        public string Description { get; set; } = string.Empty;
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        public virtual ICollection<ProductEntry> ProductEntries { get; set; } = new List<ProductEntry>();
    }
}