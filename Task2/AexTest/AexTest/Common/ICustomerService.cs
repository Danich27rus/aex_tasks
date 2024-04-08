using AexTest.Models;

namespace AexTest.Common;

public interface ICustomerService
{
    // Customers Services
    Task<List<Customer>> GetCustomersAsync(); // GET All Customers
    Task<Customer> GetCustomerAsync(int id); // GET Single Customers
    void GetCustomerInfoAsync(Customer customer);// GET Customer Orders
    Task<Customer> AddCustomerAsync(Customer customer); // POST New Customers
    Task<Customer> UpdateCustomerAsync(Customer customer); // PUT Customers
    Task<(bool, string)> DeleteCustomerAsync(Customer customer); // DELETE Customers
}
