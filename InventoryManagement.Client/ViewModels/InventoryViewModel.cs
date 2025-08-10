using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Client.ViewModels
{
    public class InventoryViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Product is required")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be non-negative")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Location is required")]
        [StringLength(100, ErrorMessage = "Location cannot exceed 100 characters")]
        public string Location { get; set; } = string.Empty;

        [Required(ErrorMessage = "Date is required")]
        public DateTime Date { get; set; }

        // Navigation properties for display
        public string? ProductName { get; set; }
        public decimal? ProductPrice { get; set; }
    }
}
