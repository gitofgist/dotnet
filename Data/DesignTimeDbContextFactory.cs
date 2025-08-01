using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace InventoryManagement.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            
            // Use the connection string directly for design-time
            optionsBuilder.UseSqlServer("Server=localhost,1433;Database=InventoryManagementDb;User Id=sa;Password=YourStrong@Passw0rd;TrustServerCertificate=true;");
            
            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}