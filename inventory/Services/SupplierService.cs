
using inventory.Entities;
using inventory.Helpers;
using Microsoft.EntityFrameworkCore;

namespace inventory.Services;

public interface ISupplierService
{
    Task<IEnumerable<Supplier>> GetAllSuppliers();
    Task<Supplier> GetSupplierById(int id);
    Task<Supplier> CreateSupplier(Supplier supplier);
    Task<Supplier> UpdateSupplier(int id, Supplier supplier);
    Task<bool> DeleteSupplier(int id);
}


public class SupplierService : ISupplierService
{
    private readonly DataContext _context;

    public SupplierService(DataContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Supplier>> GetAllSuppliers()
    {
        return await _context.Suppliers.ToListAsync();
    }

    public async Task<Supplier> GetSupplierById(int id)
    {
        return await _context.Suppliers.FindAsync(id);
    }

    public async Task<Supplier> CreateSupplier(Supplier supplier)
    {
        _context.Suppliers.Add(supplier);
        await _context.SaveChangesAsync();
        return supplier;
    }

    public async Task<Supplier> UpdateSupplier(int id, Supplier supplier)
    {
        var existingSupplier = await _context.Suppliers.FindAsync(id);
        if (existingSupplier == null)
        {
            throw new ApplicationException($"Supplier with id {id} not found");
        }

        existingSupplier.Name = supplier.Name;
        existingSupplier.Alamat = supplier.Alamat;
        existingSupplier.Email = supplier.Email;
        existingSupplier.Telepon = supplier.Telepon;

        await _context.SaveChangesAsync();
        return existingSupplier;
    }

    public async Task<bool> DeleteSupplier(int id)
    {
        var supplier = await _context.Suppliers.FindAsync(id);
        if (supplier == null)
        {
            return false;
        }

        _context.Suppliers.Remove(supplier);
        await _context.SaveChangesAsync();
        return true;
    }
}
