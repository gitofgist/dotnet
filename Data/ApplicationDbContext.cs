using Microsoft.EntityFrameworkCore;
using InventoryManagement.Server.Inventory;
using InventoryManagement.Server.Product;
using InventoryManagement.Server.Vendor;
using InventoryManagement.Server.VendorProduct;
using InventoryManagement.Interface.Inventory;
using InventoryManagement.Interface.Product;
using InventoryManagement.Interface.Vendor;
using InventoryManagement.Interface.ProductEntry;
using InventoryManagement.Interface.VendorProduct;

namespace InventoryManagement.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        
        // Parameterless constructor for design-time only
        public ApplicationDbContext()
        {
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Only configure if not already configured (for design-time)
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost,1433;Database=InventoryManagementDb;User Id=sa;Password=YourStrong@Passw0rd;TrustServerCertificate=true;");
            }
        }
        
        // DbSets
        public DbSet<InventoryManagement.Server.Inventory.Inventory> Inventories { get; set; }
        public DbSet<InventoryManagement.Server.Product.Product> Products { get; set; }          // NEW
        public DbSet<InventoryManagement.Server.Product.ProductEntry> ProductEntries { get; set; }
        public DbSet<InventoryManagement.Server.Vendor.Vendor> Vendors { get; set; }
        public DbSet<InventoryManagement.Server.VendorProduct.VendorProduct> VendorProducts { get; set; }  // NEW
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // EXISTING RELATIONSHIPS - keep these exactly as they are
            
            // ProductEntry -> Inventory relationship
            modelBuilder.Entity<InventoryManagement.Server.Product.ProductEntry>()
                .HasOne(p => p.Inventory)
                .WithMany(i => i.ProductEntries)
                .HasForeignKey(p => p.InventoryId)
                .OnDelete(DeleteBehavior.Cascade);
                
            // ProductEntry -> Vendor relationship (KEEP for backward compatibility)
            modelBuilder.Entity<InventoryManagement.Server.Product.ProductEntry>()
                .HasOne(p => p.Vendor)
                .WithMany(v => v.ProductEntries)
                .HasForeignKey(p => p.VendorId)
                .OnDelete(DeleteBehavior.Restrict);
                
            // Precision for existing Quantity
            modelBuilder.Entity<InventoryManagement.Server.Product.ProductEntry>()
                .Property(p => p.Quantity)
                .HasPrecision(18, 2);
                
            // NEW RELATIONSHIPS for Many-to-Many
            
            // VendorProduct -> Vendor
            modelBuilder.Entity<InventoryManagement.Server.VendorProduct.VendorProduct>()
                .HasOne(vp => vp.Vendor)
                .WithMany(v => v.VendorProducts)
                .HasForeignKey(vp => vp.VendorId)
                .OnDelete(DeleteBehavior.Cascade);
                
            // VendorProduct -> Product
            modelBuilder.Entity<InventoryManagement.Server.VendorProduct.VendorProduct>()
                .HasOne(vp => vp.Product)
                .WithMany(p => p.VendorProducts)
                .HasForeignKey(vp => vp.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
                
            // Optional: ProductEntry -> Product (for new entries)
            modelBuilder.Entity<InventoryManagement.Server.Product.ProductEntry>()
                .HasOne(pe => pe.Product)
                .WithMany(p => p.ProductEntries)
                .HasForeignKey(pe => pe.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
                
            // Optional: ProductEntry -> VendorProduct (for new entries)
            modelBuilder.Entity<InventoryManagement.Server.Product.ProductEntry>()
                .HasOne(pe => pe.VendorProduct)
                .WithMany()
                .HasForeignKey(pe => pe.VendorProductId)
                .OnDelete(DeleteBehavior.Restrict);
                
            // Configure decimal precision for new properties
            modelBuilder.Entity<InventoryManagement.Server.Product.ProductEntry>()
                .Property(pe => pe.UnitPrice)
                .HasPrecision(18, 2);
                
            modelBuilder.Entity<InventoryManagement.Server.VendorProduct.VendorProduct>()
                .Property(vp => vp.Price)
                .HasPrecision(18, 2);
                
            modelBuilder.Entity<InventoryManagement.Server.VendorProduct.VendorProduct>()
                .Property(vp => vp.MinOrderQuantity)
                .HasPrecision(18, 2);
                
            // Ensure unique vendor-product combinations
            modelBuilder.Entity<InventoryManagement.Server.VendorProduct.VendorProduct>()
                .HasIndex(vp => new { vp.VendorId, vp.ProductId })
                .IsUnique();
                
            base.OnModelCreating(modelBuilder);
        }
    }
}
