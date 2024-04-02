using AexTest.Models;
using Microsoft.EntityFrameworkCore;

namespace AexTest.Data;

public class DbInitializer
{
    private readonly ModelBuilder _builder;

    public DbInitializer(ModelBuilder builder)
    {
        _builder = builder;
    }

    public void Seed()
    {
        _builder.Entity<Manager>(a =>
        {
            a.HasData(new Manager
            {
                ID = 1,
                Name = "Даниил"
            });
            a.HasData(new Manager
            {
                ID = 2,
                Name = "Артём"
            });
            a.HasData(new Manager
            {
                ID = 3,
                Name = "Илья"
            });
        });

        _builder.Entity<Customer>(a =>
        {
            a.HasData(new Customer
            {
                ID = 1,
                Name = "Геннадий",
                ManagerID = 2
            });
            a.HasData(new Customer
            {
                ID = 2,
                Name = "Алексей",
                ManagerID = 3
            });
            a.HasData(new Customer
            {
                ID = 3,
                Name = "Иннокентий",
                ManagerID = 1
            });
        });

        _builder.Entity<Order>(a =>
        {
            a.HasData(new Order
            {
                ID = 1,
                Date = new DateTime(2022, 1, 1, 0, 0, 0),
                Amount = 100,
                CustomerID = 1
            });
            a.HasData(new Order
            {
                ID = 2,
                Date = new DateTime(2024, 1, 1, 0, 0, 0),
                Amount = 1000,
                CustomerID = 2
            });
            a.HasData(new Order
            {
                ID = 3,
                Date = new DateTime(2023, 1, 1, 0, 0, 0),
                Amount = 100,
                CustomerID = 3
            });
        });
    }
}
