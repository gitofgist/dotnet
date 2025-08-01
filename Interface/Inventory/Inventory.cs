using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Interface.Inventory
{
    public interface Inventory
    {
        int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        string Name { get; set; }
        
        [StringLength(500)]
        string Description { get; set; }
        
        DateTime CreatedAt { get; set; }
        
        ICollection<InventoryManagement.Server.Product.ProductEntry> ProductEntries { get; set; }
    }
} 