using inventory.Entities;
using inventory.Helpers;
using Microsoft.EntityFrameworkCore;

namespace inventory.Services;

public interface IProductMasukService
{
    Task<ProductMasuk> GetProductMasukById(int id);
    Task<IEnumerable<ProductMasuk>> GetAllProductMasuk();
    Task<ProductMasuk> CreateProductMasuk(ProductMasuk productMasuk);
    Task<ProductMasuk> UpdateProductMasuk(int id, ProductMasuk productMasuk);
    Task<bool> DeleteProductMasuk(int id);
}

public class ProductMasukService : IProductMasukService
{
    private readonly DataContext _context;

    public ProductMasukService(DataContext context)
    {
        _context = context;
    }

    public async Task<ProductMasuk> GetProductMasukById(int id)
    {
        return await _context.ProductMasuks.FindAsync(id);
    }

    public async Task<IEnumerable<ProductMasuk>> GetAllProductMasuk()
    {
        return await _context.ProductMasuks.ToListAsync();
    }

    public async Task<ProductMasuk> CreateProductMasuk(ProductMasuk productMasuk)
    {
        _context.ProductMasuks.Add(productMasuk);
        await _context.SaveChangesAsync();
        return productMasuk;
    }

    public async Task<ProductMasuk> UpdateProductMasuk(int id, ProductMasuk productMasuk)
    {
        var existingProductMasuk = await _context.ProductMasuks.FindAsync(id);
        if (existingProductMasuk == null)
        {
            throw new ApplicationException("ProductMasuk not found");
        }

        existingProductMasuk.Name = productMasuk.Name;
        existingProductMasuk.Qty = productMasuk.Qty;
        existingProductMasuk.Product = productMasuk.Product;
        existingProductMasuk.ProductID = productMasuk.ProductID;
        existingProductMasuk.Supplier = productMasuk.Supplier;
        existingProductMasuk.SupplierID = productMasuk.SupplierID;

        await _context.SaveChangesAsync();
        return existingProductMasuk;
    }

    public async Task<bool> DeleteProductMasuk(int id)
    {
        var productMasuk = await _context.ProductMasuks.FindAsync(id);
        if (productMasuk == null)
        {
            return false;
        }

        _context.ProductMasuks.Remove(productMasuk);
        await _context.SaveChangesAsync();
        return true;
    }
}
