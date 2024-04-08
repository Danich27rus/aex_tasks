using AexTest.Common;
using AexTest.Models;
using Microsoft.AspNetCore.Mvc;

namespace AexTest.Controllers;

public class InfoController : ControllerBase
{
    private readonly ICustomerService _customersService;
    private readonly IOrderService _ordersService;

    public InfoController(ICustomerService customersService, IOrderService orderService)
    {
        _customersService = customersService;
        _ordersService = orderService;
    }

    [HttpGet("byDateAndId")]
    public async Task<IActionResult> GetCustomers(DateTime beginDate, int sumAmount)
    {
        List<CustomerViewModel> resultCustomers = new();

        var orders = await _ordersService.GetOrderOverAmountAsync(sumAmount);

        if (orders == null || orders.Count == 0)
        {
            return StatusCode(StatusCodes.Status204NoContent);
        }

        var customerIds = orders.Where(o => o.Date >= beginDate).Select(o => o.CustomerID).Distinct();

        foreach (int id in customerIds)
        {
            Customer customer = await _customersService.GetCustomerAsync(id);
            if (customer == null)
            {
                continue;
            }

            _customersService.GetCustomerInfoAsync(customer);
            var customerViewModels = customer.Orders.Select(o => new CustomerViewModel
            {
                CustomerName = customer.Name,
                ManagerName = customer.Manager.Name,
                Amount = o.Amount
            });

            resultCustomers.AddRange(customerViewModels);
        }

        if (resultCustomers.Count == 0)
        {
            return StatusCode(StatusCodes.Status204NoContent);
        }

        return StatusCode(StatusCodes.Status200OK, resultCustomers);
    }
}
