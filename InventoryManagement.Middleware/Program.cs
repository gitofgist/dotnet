using Microsoft.EntityFrameworkCore;
using InventoryManagement.Service.Data;
using InventoryManagement.Service.Server.Inventory;
using InventoryManagement.Service.Server.Product;
using InventoryManagement.Service.Server.Vendor;
using InventoryManagement.Service.Server.VendorProduct;
using InventoryManagement.Service.BusinessLayer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllersWithViews();
builder.Services.AddControllers();

// Add Entity Framework with connection string
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register Service Layer implementations
builder.Services.AddScoped<IInventory, InventoryImpl>();
builder.Services.AddScoped<IProduct, ProductImpl>();
builder.Services.AddScoped<IProductEntry, ProductEntryImpl>();
builder.Services.AddScoped<IVendor, VendorImpl>();
builder.Services.AddScoped<IVendorProduct, VendorProductImpl>();

// Register Business Layer managers
builder.Services.AddScoped<InventoryManager>();
builder.Services.AddScoped<ProductManager>();
builder.Services.AddScoped<VendorManager>();
builder.Services.AddScoped<VendorProductManager>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// Map MVC routes
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Map API routes
app.MapControllers();

app.Run();
