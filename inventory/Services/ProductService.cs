using inventory.Entities;
using inventory.Helpers;
using Microsoft.EntityFrameworkCore;

namespace inventory.Services;

public interface IProductService
{
    Task<Product> GetProductById(int id);
    Task<IEnumerable<Product>> GetAllProducts();
    Task<Product> CreateProduct(Product product);
    Task<Product> UpdateProduct(int id, Product product);
    Task<bool> DeleteProduct(int id);
}


public class ProductService : IProductService
{
    private readonly DataContext _context;

    public ProductService(DataContext context)
    {
        _context = context;
    }

    public async Task<Product> GetProductById(int id)
    {
        return await _context.Products.FindAsync(id);
    }

    public async Task<IEnumerable<Product>> GetAllProducts()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<Product> CreateProduct(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<Product> UpdateProduct(int id, Product product)
    {
        var existingProduct = await _context.Products.FindAsync(id);
        if (existingProduct == null)
        {
            throw new ApplicationException("Product not found.");
        }

        existingProduct.Name = product.Name;
        existingProduct.Image = product.Image;
        existingProduct.Qty = product.Qty;
        existingProduct.Category = product.Category;
        existingProduct.CategoryID = product.CategoryID;

        await _context.SaveChangesAsync();
        return existingProduct;
    }

    public async Task<bool> DeleteProduct(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return false;
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return true;
    }
}
