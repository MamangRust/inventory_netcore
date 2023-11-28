using inventory.Entities;
using inventory.Helpers;
using Microsoft.EntityFrameworkCore;

namespace inventory.Services;

public interface ICustomerService
{
    Task<Customer> GetCustomerById(int id);
    Task<IEnumerable<Customer>> GetAllCustomers();
    Task<Customer> CreateCustomer(Customer customer);
    Task<Customer> UpdateCustomer(int id, Customer customer);
    Task<bool> DeleteCustomer(int id);
}

public class CustomerService : ICustomerService
{
    private readonly DataContext _context;

    public CustomerService(DataContext context)
    {
        _context = context;
    }

    public async Task<Customer> GetCustomerById(int id)
    {
        return await _context.Customers.FindAsync(id);
    }

    public async Task<IEnumerable<Customer>> GetAllCustomers()
    {
        return await _context.Customers.ToListAsync();
    }

    public async Task<Customer> CreateCustomer(Customer customer)
    {
        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();
        return customer;
    }

    public async Task<Customer> UpdateCustomer(int id, Customer customer)
    {
        var existingCustomer = await _context.Customers.FindAsync(id);
        if (existingCustomer == null)
        {
            throw new ApplicationException("Customer not found.");
        }

        existingCustomer.Name = customer.Name;
        existingCustomer.Alamat = customer.Alamat;
        existingCustomer.Email = customer.Email;
        existingCustomer.Telepon = customer.Telepon;

        await _context.SaveChangesAsync();
        return existingCustomer;
    }

    public async Task<bool> DeleteCustomer(int id)
    {
        var customer = await _context.Customers.FindAsync(id);
        if (customer == null)
        {
            return false;
        }

        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync();
        return true;
    }
}
