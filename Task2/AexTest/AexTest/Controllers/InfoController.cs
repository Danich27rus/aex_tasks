﻿using AexTest.Common;
using AexTest.Models;
using Microsoft.AspNetCore.Mvc;

namespace AexTest.Controllers;

public class InfoController : ControllerBase
{
    private readonly ICustomerService _customersService;
    private readonly IOrderService _ordersService;
    private readonly IManagerService _managersService;

    public InfoController(ICustomerService customersService, IOrderService orderService, IManagerService managerService)
    {
        _customersService = customersService;
        _ordersService = orderService;
        _managersService = managerService;
    }

    [HttpGet("byDateAndId")]
    public async Task<IActionResult> GetCustomers(DateTime beginDate, int sumAmount)
    {
        var customers = await _customersService.GetCustomersAsync();

        if (customers == null || customers.Count == 0)
        {
            return StatusCode(StatusCodes.Status204NoContent);
        }

        var filteredCustomers = customers.Where(c =>
            c.Orders.Any(o => o.Date >= beginDate && o.Amount >= sumAmount)
        ).ToList();

        var customerViewModels = filteredCustomers.SelectMany(c => c.Orders.Where(o => o.Date >= beginDate && o.Amount >= sumAmount)
                                           .Select(o => new CustomerViewModel
                                           {
                                               CustomerName = c.Name,
                                               ManagerName = c.Manager?.Name,
                                               Amount = o.Amount
                                           })).ToList();

        if (customerViewModels.Count == 0)
        {
            return StatusCode(StatusCodes.Status204NoContent);
        }

        return StatusCode(StatusCodes.Status200OK, customerViewModels);
    }
}
