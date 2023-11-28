using inventory.Entities;
using inventory.Helpers;
using Microsoft.EntityFrameworkCore;


namespace inventory.Services;

public interface ISaleService
{
    Task<Sale> GetSaleById(int id);
    Task<IEnumerable<Sale>> GetAllSales();
    Task<Sale> CreateSale(Sale sale);
    Task<Sale> UpdateSale(int id, Sale sale);
    Task<bool> DeleteSale(int id);
}

public class SaleService : ISaleService
{
    private readonly DataContext _context;

    public SaleService(DataContext context)
    {
        _context = context;
    }

    public async Task<Sale> GetSaleById(int id)
    {
        return await _context.Sales.FindAsync(id);
    }

    public async Task<IEnumerable<Sale>> GetAllSales()
    {
        return await _context.Sales.ToListAsync();
    }

    public async Task<Sale> CreateSale(Sale sale)
    {
        _context.Sales.Add(sale);
        await _context.SaveChangesAsync();
        return sale;
    }

    public async Task<Sale> UpdateSale(int id, Sale sale)
    {
        var existingSale = await _context.Sales.FindAsync(id);
        if (existingSale == null)
        {
            throw new ApplicationException($"Sale with id {id} not found");
        }

        existingSale.Name = sale.Name;
        existingSale.Alamat = sale.Alamat;
        existingSale.Email = sale.Email;
        existingSale.Telepon = sale.Telepon;

        await _context.SaveChangesAsync();

        return existingSale;
    }

    public async Task<bool> DeleteSale(int id)
    {
        var sale = await _context.Sales.FindAsync(id);
        if (sale == null)
        {
            return false;
        }

        _context.Sales.Remove(sale);
        await _context.SaveChangesAsync();
        return true;
    }


}