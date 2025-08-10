using Microsoft.EntityFrameworkCore;
using InventoryManagement.Service.Models;
using InventoryManagement.Service.Server.Inventory;
using InventoryManagement.Service.Server.Product;
using InventoryManagement.Service.Server.Vendor;
using InventoryManagement.Service.Server.VendorProduct;

namespace InventoryManagement.Service.Data
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
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Product> Products { get; set; }          // NEW
        public DbSet<ProductEntry> ProductEntries { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<VendorProduct> VendorProducts { get; set; }  // NEW
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // EXISTING RELATIONSHIPS - keep these exactly as they are
            
            // ProductEntry -> Inventory relationship
            modelBuilder.Entity<ProductEntry>()
                .HasOne(p => p.Inventory)
                .WithMany(i => i.ProductEntries)
                .HasForeignKey(p => p.InventoryId)
                .OnDelete(DeleteBehavior.Cascade);
                
            // ProductEntry -> Vendor relationship (KEEP for backward compatibility)
            modelBuilder.Entity<ProductEntry>()
                .HasOne(p => p.Vendor)
                .WithMany(v => v.ProductEntries)
                .HasForeignKey(p => p.VendorId)
                .OnDelete(DeleteBehavior.Restrict);
                
            // Precision for existing Quantity
            modelBuilder.Entity<ProductEntry>()
                .Property(p => p.Quantity)
                .HasPrecision(18, 2);
                
            // NEW RELATIONSHIPS for Many-to-Many
            
            // VendorProduct -> Vendor
            modelBuilder.Entity<VendorProduct>()
                .HasOne(vp => vp.Vendor)
                .WithMany(v => v.VendorProducts)
                .HasForeignKey(vp => vp.VendorId)
                .OnDelete(DeleteBehavior.Cascade);
                
            // VendorProduct -> Product
            modelBuilder.Entity<VendorProduct>()
                .HasOne(vp => vp.Product)
                .WithMany(p => p.VendorProducts)
                .HasForeignKey(vp => vp.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
                
            // Optional: ProductEntry -> Product (for new entries)
            modelBuilder.Entity<ProductEntry>()
                .HasOne(pe => pe.Product)
                .WithMany(p => p.ProductEntries)
                .HasForeignKey(pe => pe.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
                
            // Optional: ProductEntry -> VendorProduct (for new entries)
            modelBuilder.Entity<ProductEntry>()
                .HasOne(pe => pe.VendorProduct)
                .WithMany()
                .HasForeignKey(pe => pe.VendorProductId)
                .OnDelete(DeleteBehavior.Restrict);
                
            // Configure decimal precision for new properties
            modelBuilder.Entity<ProductEntry>()
                .Property(pe => pe.UnitPrice)
                .HasPrecision(18, 2);
                
            modelBuilder.Entity<VendorProduct>()
                .Property(vp => vp.Price)
                .HasPrecision(18, 2);
                
            modelBuilder.Entity<VendorProduct>()
                .Property(vp => vp.MinOrderQuantity)
                .HasPrecision(18, 2);
                
            // Ensure unique vendor-product combinations
            modelBuilder.Entity<VendorProduct>()
                .HasIndex(vp => new { vp.VendorId, vp.ProductId })
                .IsUnique();
                
            base.OnModelCreating(modelBuilder);
        }
    }
}
