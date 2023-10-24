
using Microsoft.EntityFrameworkCore;

namespace GroupAssignment1.Models;

public static class DBInit
{
    public static void Seed(IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
        HousingDbContext context = serviceScope.ServiceProvider.GetRequiredService<HousingDbContext>();
        // context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        if (!context.Housings.Any())
        {
            var Housings = new List<Housing>
            {
                new Housing
                {
                    Name = "Test1",
                    Rent = 250,
                    Description = "noenoenoeo" ,
                    OwnerId = 1,
                    ImageUrl = "https://images.pexels.com/photos/2581922/pexels-photo-2581922.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"

                },
                new Housing
                {
                    Name = "Test2",
                    Rent = 500,
                    Description = "noenoenoeo",
                    OwnerId = 2,
                    ImageUrl = "https://images.pexels.com/photos/463734/pexels-photo-463734.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                new Housing
                {
                    Name = "Test3",
                    Rent = 750,
                    Description = "noenoenoeo" ,
                    OwnerId = 2,
                    ImageUrl = "https://images.pexels.com/photos/2183521/pexels-photo-2183521.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                }
                
            };
            context.AddRange(Housings);
            context.SaveChanges();
        }

        if (!context.Customers.Any())
        {
            var customers = new List<Customer>
            {
                new Customer { FirstName = "Alice", LastName = "Hansen", Email = "Alice@gmail.com", Phone = "458 11 111"},
                new Customer { FirstName = "Bob", LastName = "Johansen", Email = "Bob@Gmail.com", Phone = "458 22 222" },
            };
            context.AddRange(customers);
            context.SaveChanges();
        }

        if (!context.Orders.Any())
        {
            var orders = new List<Order>
            {
                new Order {StartDate = DateTime.Today.ToString(), EndDate = DateTime.Today.AddDays(14).ToString(), CustomerId = 1, HousingId = 1},
                new Order {StartDate = DateTime.Today.ToString(), EndDate = DateTime.Today.AddDays(14).ToString(), CustomerId = 2, HousingId = 2},
            };
            
            foreach (var order in orders)
            {
                var housing = context.Housings.Find(order.HousingId);

                TimeSpan timeSpan = DateTime.Parse(order.EndDate) - DateTime.Parse(order.StartDate);
                int daysDiff = timeSpan.Days;

                order.TotalPrice = daysDiff * housing?.Rent ?? 0;
            }
            context.AddRange(orders);
            context.SaveChanges();
        }
    }
}
