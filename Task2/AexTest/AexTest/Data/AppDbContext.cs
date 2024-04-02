using AexTest.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace AexTest.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Manager> Managers { get; set; }
    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {

        // Define relationship between each table
        builder.Entity<Customer>()
            .HasOne(c => c.Manager)
            .WithMany()
            .HasForeignKey(m => m.ManagerID);

        builder.Entity<Order>()
            .HasOne(o => o.Customer)
            .WithMany(c => c.Orders)
            .HasForeignKey(o => o.CustomerID);

        // Seed database with authors and books for demo
        new DbInitializer(builder).Seed();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
    }
}
