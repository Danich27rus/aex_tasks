using AexTest.Common;
using AexTest.Data;
using AexTest.Models;
using Microsoft.EntityFrameworkCore;

namespace AexTest.Service;

public class OrderService : IOrderService
{

    private readonly AppDbContext _db;

    public OrderService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<Order> AddOrderAsync(Order order)
    {
        try
        {
            await _db.Orders.AddAsync(order);
            await _db.SaveChangesAsync();
            return await _db.Orders.FindAsync(order.ID); // Auto ID from DB
        }
        catch (Exception ex)
        {
            return null; // An error occured
        }
    }

    public async Task<(bool, string)> DeleteOrderAsync(Order order)
    {
        try
        {
            var dbOrder = await _db.Orders.FindAsync(order.ID);

            if (dbOrder == null)
            {
                return (false, "Order could not be found");
            }

            _db.Orders.Remove(order);
            await _db.SaveChangesAsync();

            return (true, "Order got deleted.");
        }
        catch (Exception ex)
        {
            return (false, $"An error occured. Error Message: {ex.Message}");
        }
    }

    public async Task<Order> GetOrderAsync(int id)
    {
        try
        {
            return await _db.Orders.FindAsync(id);
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<List<Order>> GetOrderOverAmountAsync(int amount)
    {
        try
        {
            return await _db.Orders.Where(o => o.Amount >= amount).ToListAsync();
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<List<Order>> GetOrdersAsync()
    {
        try
        {
            return await _db.Orders.ToListAsync();
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<Order> UpdateOrderAsync(Order order)
    {
        try
        {
            _db.Entry(order).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            return order;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
}
