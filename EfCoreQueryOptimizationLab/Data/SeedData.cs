using EfCoreQueryOptimizationLab.Models;

namespace EfCoreQueryOptimizationLab.Data;

public static class SeedData
{
    public static void Initialize(AppDbContext db)
    {
        if (db.Customers.Any())
        {
            return;
        }

        var products = new List<Product>();

        for (var i = 1; i <= 50; i++)
        {
            products.Add(new Product
            {
                Name = $"Product {i}",
                Category = $"Category {(i % 5) + 1}",
                Description =
                    $"Long product description for product {i}. " +
                    new string('x', 200),
                Price = 10 + i
            });
        }

        db.Products.AddRange(products);
        db.SaveChanges();

        for (var customerIndex = 1;
             customerIndex <= 30;
             customerIndex++)
        {
            var customer = new Customer
            {
                Name = $"Customer {customerIndex}",
                Email = $"customer{customerIndex}@example.com",
                Region = $"Region {(customerIndex % 4) + 1}"
            };

            for (var orderIndex = 1;
                 orderIndex <= 5;
                 orderIndex++)
            {
                var order = new Order
                {
                    CreatedAt = DateTime.UtcNow
                        .AddDays(-(customerIndex * orderIndex)),

                    Status = orderIndex % 2 == 0
                        ? "Completed"
                        : "Processing",

                    InternalNotes =
                        "Internal order notes " +
                        new string('n', 100)
                };

                for (var itemIndex = 1;
                     itemIndex <= 10;
                     itemIndex++)
                {
                    var productIndex =
                        (customerIndex +
                         orderIndex +
                         itemIndex)
                        % products.Count;

                    var product = products[productIndex];

                    order.OrderItems.Add(new OrderItem
                    {
                        Product = product,
                        Quantity = (itemIndex % 3) + 1,
                        UnitPrice = product.Price
                    });
                }

                for (var paymentIndex = 1;
                     paymentIndex <= 3;
                     paymentIndex++)
                {
                    order.Payments.Add(new Payment
                    {
                        Amount = 50 + paymentIndex * 10,
                        PaidAt = DateTime.UtcNow,
                        Method = paymentIndex % 2 == 0
                            ? "Card"
                            : "PayPal",
                        ProviderReference =
                            $"REF-{customerIndex}-{orderIndex}-{paymentIndex}"
                    });
                }

                customer.Orders.Add(order);
            }

            db.Customers.Add(customer);
        }

        db.SaveChanges();
    }
}