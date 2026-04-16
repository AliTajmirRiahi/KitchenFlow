using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Order;
using Restaurant.Infrastructure.Persistence;
using Restaurant.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Unit.Test.Infrastructure.Repositories
{
    public class OrderRepositoryTests
    {
        private RestaurantDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<RestaurantDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()) 
                .Options;

            return new RestaurantDbContext(options);
        }

        [Fact]
        public async Task AddAsync_Should_Save_Order_And_Return_Id()
        {
            // Arrange
            using var context = CreateDbContext();
            var repository = new OrderRepository(context);

            var order = new Order(customerId: 1, tableId: 2);
            order.AddItem(productId: 100, quantity: 2, unitPrice: 50);

            // Act
            var id = await repository.AddAsync(order);

            // Assert
            var savedOrder = await context.Orders.FirstOrDefaultAsync(o => o.Id == id);

            // Assert Order
            Assert.NotNull(savedOrder);
            Assert.Equal(order.CustomerId, savedOrder.CustomerId);
            Assert.Equal(order.TableId, savedOrder.TableId);
            Assert.Equal(order.Status, savedOrder.Status);
            Assert.Single(order.Items);

            // Assert OrderItem
            var item = savedOrder.Items.First();
            Assert.Equal(100, item.ProductId);
            Assert.Equal(2, item.Quantity);
            Assert.Equal(50, item.UnitPrice);
            Assert.Equal(100, item.Total);
        }
    }
}
