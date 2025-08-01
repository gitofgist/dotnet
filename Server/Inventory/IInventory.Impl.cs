using System.ComponentModel.DataAnnotations;
using InventoryManagement.Interface.Inventory;
using InventoryManagement.Server.Inventory;

namespace InventoryManagement.Server.Inventory
{
    public class Inventory : InventoryManagement.Interface.Inventory.Inventory
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [StringLength(500)]
        public string Description { get; set; } = string.Empty;
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        public virtual ICollection<InventoryManagement.Server.Product.ProductEntry> ProductEntries { get; set; } = new List<InventoryManagement.Server.Product.ProductEntry>();
    }
}