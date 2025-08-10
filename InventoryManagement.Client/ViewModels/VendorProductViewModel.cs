using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Client.ViewModels
{
    public class VendorProductViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Vendor is required")]
        public int VendorId { get; set; }

        [Required(ErrorMessage = "Product is required")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be non-negative")]
        public decimal Price { get; set; }

        [StringLength(100, ErrorMessage = "SKU cannot exceed 100 characters")]
        public string? SKU { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        // Navigation properties for display
        public string? VendorName { get; set; }
        public string? ProductName { get; set; }
        public string? ProductDescription { get; set; }
    }
}
