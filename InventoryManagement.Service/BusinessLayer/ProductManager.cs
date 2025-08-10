using InventoryManagement.Service.Models;
using InventoryManagement.Service.Server.Product;

namespace InventoryManagement.Service.BusinessLayer
{
    public class ProductManager
    {
        private readonly IProduct _productService;

        public ProductManager(IProduct productService)
        {
            _productService = productService;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _productService.GetAllAsync();
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _productService.GetByIdAsync(id);
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            // Add business logic validation here
            if (string.IsNullOrWhiteSpace(product.Name))
                throw new ArgumentException("Product name cannot be empty");

            return await _productService.CreateAsync(product);
        }

        public async Task<Product> UpdateProductAsync(Product product)
        {
            // Add business logic validation here
            if (string.IsNullOrWhiteSpace(product.Name))
                throw new ArgumentException("Product name cannot be empty");

            return await _productService.UpdateAsync(product);
        }

        public async Task DeleteProductAsync(int id)
        {
            await _productService.DeleteAsync(id);
        }
    }
}
