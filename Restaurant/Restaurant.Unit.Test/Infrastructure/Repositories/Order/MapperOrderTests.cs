using Restaurant.Domain.Contract.Order;
using Restaurant.Domain.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Unit.Test.Infrastructure.Repositories
{
    public class MapperOrderTests
    {
        [Fact]
        public void CustomMapOrderDtoToOrder_Should_Map_All_Properties_And_Items()
        {
            // Arrange
            var mapper = new Mapper();
            var orderDto = new OrderDto
            {
                CustomerId = 1,
                TableId = 10,
                Items = new[]
                {
                    new OrderItemDto { ProductId = 100, Quantity = 2, UnitPrice = 50 },
                    new OrderItemDto { ProductId = 101, Quantity = 1, UnitPrice = 30 }
                }.ToList()
            };

            // Act
            var order = mapper.CustomMapOrderDtoToOrder(orderDto);

            // Assert
            Assert.Equal(orderDto.CustomerId, order.CustomerId);
            Assert.Equal(orderDto.TableId, order.TableId);
            Assert.Equal(orderDto.Items.Count, order.Items.Count);

            var items = order.Items.ToList();

            for (int i = 0; i < order.Items.Count; i++)
            {
                Assert.Equal(orderDto.Items[i].ProductId, items[i].ProductId);
                Assert.Equal(orderDto.Items[i].Quantity, items[i].Quantity);
                Assert.Equal(orderDto.Items[i].UnitPrice, items[i].UnitPrice);
            }
        }

        [Fact]
        public void CustomMapOrderToOrderDto_Should_Map_All_Properties_And_Items()
        {
            // Arrange
            var mapper = new Mapper();
            var order = new Order(1, 10);
            order.AddItem(100, 2, 50);
            order.AddItem(101, 1, 30);

            // Act
            var orderDto = mapper.CustomMapOrderToOrderDto(order);

            // Assert
            Assert.Equal(order.CustomerId, orderDto.CustomerId);
            Assert.Equal(order.TableId, orderDto.TableId);
            Assert.Equal(order.Items.Count, orderDto.Items.Count);

            var items = order.Items.ToList();

            for (int i = 0; i < orderDto.Items.Count; i++)
            {
                Assert.Equal(items[i].ProductId, orderDto.Items[i].ProductId);
                Assert.Equal(items[i].Quantity, orderDto.Items[i].Quantity);
                Assert.Equal(items[i].UnitPrice, orderDto.Items[i].UnitPrice);
            }
        }
    }
}
