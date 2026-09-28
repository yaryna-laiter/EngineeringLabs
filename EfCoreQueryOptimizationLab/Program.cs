using System.Diagnostics;
using EfCoreQueryOptimizationLab.Data;
using EfCoreQueryOptimizationLab.Diagnostics;
using EfCoreQueryOptimizationLab.Dtos;
using EfCoreQueryOptimizationLab.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<QueryCounterInterceptor>();

builder.Services.AddDbContext<AppDbContext>((serviceProvider, options) =>
{
    var interceptor =
        serviceProvider.GetRequiredService<QueryCounterInterceptor>();

    options
        .UseLazyLoadingProxies()
        .UseSqlite("Data Source=efcore-query-lab.db")
        .AddInterceptors(interceptor)

        .LogTo(Console.WriteLine, LogLevel.Information)

        .EnableDetailedErrors()

        .EnableSensitiveDataLogging();
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db =
        scope.ServiceProvider.GetRequiredService<AppDbContext>();

    db.Database.EnsureDeleted();
    db.Database.EnsureCreated();

    SeedData.Initialize(db);
}

app.MapGet("/", () =>
{
    return Results.Ok(new
    {
        Message = "EF Core Query Optimization Lab",
        Endpoints = new[]
        {
            "/api/query/lazy",
            "/api/query/eager",
            "/api/query/projection"
        }
    });
});

app.MapGet(
    "/api/query/lazy",
    async (
        AppDbContext db,
        QueryCounterInterceptor counter) =>
    {
        counter.Reset();

        var stopwatch = Stopwatch.StartNew();

        var customers = await db.Customers
            .OrderBy(c => c.Id)
            .Take(20)
            .ToListAsync();

        var data = customers
            .Select(MapCustomer)
            .ToList();

        stopwatch.Stop();

        return Results.Ok(new
        {
            Approach = "Lazy Loading",
            DatabaseRoundtrips = counter.QueryCount,
            ExecutionTimeMs =
                Math.Round(stopwatch.Elapsed.TotalMilliseconds, 2),
            CustomersReturned = data.Count,
            Data = data
        });
    });

app.MapGet(
    "/api/query/eager",
    async (
        AppDbContext db,
        QueryCounterInterceptor counter) =>
    {
        counter.Reset();

        var stopwatch = Stopwatch.StartNew();

        var customers = await db.Customers
            .OrderBy(c => c.Id)
            .Take(20)

            .Include(c => c.Orders)
                .ThenInclude(o => o.OrderItems)
                    .ThenInclude(i => i.Product)

            .Include(c => c.Orders)
                .ThenInclude(o => o.Payments)

            .AsSingleQuery()

            .ToListAsync();

        var data = customers
            .Select(MapCustomer)
            .ToList();

        stopwatch.Stop();

        return Results.Ok(new
        {
            Approach = "Eager Loading / Include",
            DatabaseRoundtrips = counter.QueryCount,
            ExecutionTimeMs =
                Math.Round(stopwatch.Elapsed.TotalMilliseconds, 2),
            CustomersReturned = data.Count,
            Data = data
        });
    });

app.MapGet(
    "/api/query/projection",
    async (
        AppDbContext db,
        QueryCounterInterceptor counter) =>
    {
        counter.Reset();

        var stopwatch = Stopwatch.StartNew();

        var data = await db.Customers
            .OrderBy(c => c.Id)
            .Take(20)

            .Select(c => new CustomerDto
            {
                Id = c.Id,
                Name = c.Name,

                Orders = c.Orders
                    .Select(o => new OrderDto
                    {
                        Id = o.Id,
                        CreatedAt = o.CreatedAt,
                        Status = o.Status,

                        ItemCount =
                            o.OrderItems.Count(),

                        TotalPaid =
                            o.Payments.Sum(p => p.Amount),

                        ProductNames =
                            o.OrderItems
                                .Select(i => i.Product.Name)
                                .ToList()
                    })
                    .ToList()
            })
            .ToListAsync();

        stopwatch.Stop();

        return Results.Ok(new
        {
            Approach = "DTO Projection",
            DatabaseRoundtrips = counter.QueryCount,
            ExecutionTimeMs =
                Math.Round(stopwatch.Elapsed.TotalMilliseconds, 2),
            CustomersReturned = data.Count,
            Data = data
        });
    });


app.Run();

static CustomerDto MapCustomer(Customer customer)
{
    return new CustomerDto
    {
        Id = customer.Id,

        Name = customer.Name,

        Orders = customer.Orders
            .Select(order => new OrderDto
            {
                Id = order.Id,

                CreatedAt = order.CreatedAt,

                Status = order.Status,

                ItemCount =
                    order.OrderItems.Count,

                TotalPaid =
                    order.Payments.Sum(p => p.Amount),

                ProductNames =
                    order.OrderItems
                        .Select(item => item.Product.Name)
                        .ToList()
            })
            .ToList()
    };
}