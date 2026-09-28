using EfCoreQueryOptimizationLab.Models;
using Microsoft.EntityFrameworkCore;

namespace EfCoreQueryOptimizationLab.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Customer> Customers => Set<Customer>();

    public DbSet<Order> Orders => Set<Order>();

    public DbSet<OrderItem> OrderItems => Set<OrderItem>();

    public DbSet<Product> Products => Set<Product>();

    public DbSet<Payment> Payments => Set<Payment>();
}