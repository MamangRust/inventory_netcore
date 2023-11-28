using inventory.Entities;
using inventory.Helpers;
using Microsoft.EntityFrameworkCore;

namespace inventory.Services;


public interface IProductKeluarService
{
    Task<ProductKeluar> GetProductKeluarById(int id);
    Task<IEnumerable<ProductKeluar>> GetAllProductKeluar();
    Task<ProductKeluar> CreateProductKeluar(ProductKeluar productKeluar);
    Task<ProductKeluar> UpdateProductKeluar(int id, ProductKeluar productKeluar);
    Task<bool> DeleteProductKeluar(int id);
}


public class ProductKeluarService : IProductKeluarService
{
    private readonly DataContext _context;

    public ProductKeluarService(DataContext context)
    {
        _context = context;
    }

    public async Task<ProductKeluar> GetProductKeluarById(int id)
    {
        return await _context.ProductKeluars.FindAsync(id);
    }

    public async Task<IEnumerable<ProductKeluar>> GetAllProductKeluar()
    {
        return await _context.ProductKeluars.ToListAsync();
    }

    public async Task<ProductKeluar> CreateProductKeluar(ProductKeluar productKeluar)
    {
        _context.ProductKeluars.Add(productKeluar);
        await _context.SaveChangesAsync();
        return productKeluar;
    }

    public async Task<ProductKeluar> UpdateProductKeluar(int id, ProductKeluar productKeluar)
    {
        var existingProduct = await _context.ProductKeluars.FindAsync(id);
        if (existingProduct == null)
        {
            throw new ApplicationException("ProductKeluar not found");
        }

        existingProduct.Qty = productKeluar.Qty;
        existingProduct.Product = productKeluar.Product;
        existingProduct.ProductID = productKeluar.ProductID;
        existingProduct.Category = productKeluar.Category;
        existingProduct.CategoryID = productKeluar.CategoryID;

        await _context.SaveChangesAsync();
        return existingProduct;
    }

    public async Task<bool> DeleteProductKeluar(int id)
    {
        var productKeluar = await _context.ProductKeluars.FindAsync(id);
        if (productKeluar == null)
        {
            return false;
        }

        _context.ProductKeluars.Remove(productKeluar);
        await _context.SaveChangesAsync();
        return true;
    }
}