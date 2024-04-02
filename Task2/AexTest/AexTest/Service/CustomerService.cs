using AexTest.Common;
using AexTest.Data;
using AexTest.Models;
using Microsoft.EntityFrameworkCore;

namespace AexTest.Service;

public class CustomerService : ICustomerService
{
    private readonly AppDbContext _db;

    public CustomerService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<Customer> AddCustomerAsync(Customer customer)
    {
        try
        {
            await _db.Customers.AddAsync(customer);
            await _db.SaveChangesAsync();
            return await _db.Customers.FindAsync(customer.ID); // Auto ID from DB
        }
        catch (Exception ex)
        {
            return null; // An error occured
        }
    }

    public async Task<(bool, string)> DeleteCustomerAsync(Customer customer)
    {
        try
        {
            var dbCustomer = await _db.Customers.FindAsync(customer.ID);

            if (dbCustomer == null)
            {
                return (false, "Customer could not be found");
            }

            _db.Customers.Remove(customer);
            await _db.SaveChangesAsync();

            return (true, "Customer got deleted.");
        }
        catch (Exception ex)
        {
            return (false, $"An error occured. Error Message: {ex.Message}");
        }
    }

    public async Task<Customer> GetCustomerAsync(int id)
    {
        try
        {
            return await _db.Customers.FindAsync(id);
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<List<Customer>> GetCustomersAsync()
    {
        try
        {
            return await _db.Customers
                .Include(c => c.Orders)
                .Include(c => c.Manager)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<Customer> UpdateCustomerAsync(Customer customer)
    {
        try
        {
            _db.Entry(customer).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            return customer;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
}
