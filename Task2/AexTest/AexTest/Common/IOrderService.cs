using AexTest.Models;

namespace AexTest.Common;

public interface IOrderService
{
    // Orders Services
    Task<List<Order>> GetOrdersAsync(); // GET All Orders
    Task<Order> GetOrderAsync(int id); // GET Single Orders
    Task<List<Order>> GetOrderOverAmountAsync(int amount); // GET Orders with Amount > (int amount)
    Task<Order> AddOrderAsync(Order order); // POST New Orders
    Task<Order> UpdateOrderAsync(Order order); // PUT Orders
    Task<(bool, string)> DeleteOrderAsync(Order order); // DELETE Orders
}
