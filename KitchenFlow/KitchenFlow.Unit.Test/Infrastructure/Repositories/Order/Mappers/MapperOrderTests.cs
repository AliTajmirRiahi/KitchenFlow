using System.Collections.Generic;
using System.Linq;
using Moq;
using Xunit;
using KitchenFlow.Domain.Order;
using KitchenFlow.Domain.Order.Mappers;
using KitchenFlow.Domain.Contract.Order;

namespace KitchenFlow.Unit.Test.Infrastructure.Repositories.Order.Mappers
{
    public class MapperOrderTests
    {
        [Fact]
        public void ToDto_Should_Map_CustomerId_TableId_And_StatusDescription()
        {
            // Arrange
            var itemMapperMock = new Mock<IMapperOrderItem>();

            var order = new KitchenFlow.Domain.Order.Order(customerId: 10, tableId: 5);

            var mapper = new MapperOrder(itemMapperMock.Object);

            // Act
            var result = mapper.ToDto(order);

            // Assert
            Assert.Equal(10, result.CustomerId);
            Assert.Equal(5, result.TableId);
            Assert.NotNull(result.Items);
            Assert.Empty(result.Items);
            Assert.NotNull(result.OrderStatusDescription);
        }

        [Fact]
        public void ToDto_Should_Map_Items_Using_ItemMapper()
        {
            // Arrange
            var itemMapperMock = new Mock<IMapperOrderItem>();

            var order = new KitchenFlow.Domain.Order.Order(customerId: 10, tableId: 5);
            order.AddItem(productId: 1, quantity: 2, unitPrice: 100m);
            order.AddItem(productId: 2, quantity: 3, unitPrice: 200m);

            itemMapperMock
                .Setup(x => x.ToDto(It.IsAny<OrderItem>()))
                .Returns<OrderItem>(item => new OrderItemDto
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice
                });

            var mapper = new MapperOrder(itemMapperMock.Object);

            // Act
            var result = mapper.ToDto(order);

            // Assert
            Assert.Equal(2, result.Items.Count);

            Assert.Equal(1, result.Items[0].ProductId);
            Assert.Equal(2, result.Items[0].Quantity);
            Assert.Equal(100m, result.Items[0].UnitPrice);

            Assert.Equal(2, result.Items[1].ProductId);
            Assert.Equal(3, result.Items[1].Quantity);
            Assert.Equal(200m, result.Items[1].UnitPrice);

            itemMapperMock.Verify(x => x.ToDto(It.IsAny<OrderItem>()), Times.Exactly(2));
        }

        [Fact]
        public void ToDto_Should_Return_Empty_Items_When_Order_Has_No_Items()
        {
            // Arrange
            var itemMapperMock = new Mock<IMapperOrderItem>();
            var order = new KitchenFlow.Domain.Order.Order(customerId: 1, tableId: 1);

            var mapper = new MapperOrder(itemMapperMock.Object);

            // Act
            var result = mapper.ToDto(order);

            // Assert
            Assert.NotNull(result.Items);
            Assert.Empty(result.Items);
            itemMapperMock.Verify(x => x.ToDto(It.IsAny<OrderItem>()), Times.Never);
        }

        [Fact]
        public void ToEntity_Should_Map_CustomerId_And_TableId()
        {
            // Arrange
            var itemMapperMock = new Mock<IMapperOrderItem>();
            var mapper = new MapperOrder(itemMapperMock.Object);

            var dto = new OrderDto
            {
                CustomerId = 20,
                TableId = 7
            };

            // Act
            var result = mapper.ToEntity(dto);

            // Assert
            Assert.Equal(20, result.CustomerId);
            Assert.Equal(7, result.TableId);
        }

        [Fact]
        public void ToEntity_Should_Map_Items_From_Dto()
        {
            // Arrange
            var itemMapperMock = new Mock<IMapperOrderItem>();
            var mapper = new MapperOrder(itemMapperMock.Object);

            var dto = new OrderDto
            {
                CustomerId = 20,
                TableId = 7,
                Items = new List<OrderItemDto>
                {
                    new OrderItemDto
                    {
                        ProductId = 1,
                        Quantity = 2,
                        UnitPrice = 100m
                    },
                    new OrderItemDto
                    {
                        ProductId = 2,
                        Quantity = 3,
                        UnitPrice = 250m
                    }
                }
            };

            // Act
            var result = mapper.ToEntity(dto);

            // Assert
            Assert.Equal(2, result.Items.Count);

            Assert.Equal(1, result.Items.ElementAt(0).ProductId);
            Assert.Equal(2, result.Items.ElementAt(0).Quantity);
            Assert.Equal(100m, result.Items.ElementAt(0).UnitPrice);

            Assert.Equal(2, result.Items.ElementAt(1).ProductId);
            Assert.Equal(3, result.Items.ElementAt(1).Quantity);
            Assert.Equal(250m, result.Items.ElementAt(1).UnitPrice);
        }

        [Fact]
        public void ToEntity_Should_Return_Order_With_No_Items_When_Dto_Items_Is_Empty()
        {
            // Arrange
            var itemMapperMock = new Mock<IMapperOrderItem>();
            var mapper = new MapperOrder(itemMapperMock.Object);

            var dto = new OrderDto
            {
                CustomerId = 5,
                TableId = 9,
                Items = new List<OrderItemDto>()
            };

            // Act
            var result = mapper.ToEntity(dto);

            // Assert
            Assert.Equal(5, result.CustomerId);
            Assert.Equal(9, result.TableId);
            Assert.Empty(result.Items);
        }

        [Fact]
        public void ToEntity_Should_Preserve_Item_Order()
        {
            // Arrange
            var itemMapperMock = new Mock<IMapperOrderItem>();
            var mapper = new MapperOrder(itemMapperMock.Object);

            var dto = new OrderDto
            {
                CustomerId = 1,
                TableId = 2,
                Items = new List<OrderItemDto>
                {
                    new OrderItemDto { ProductId = 100, Quantity = 1, UnitPrice = 10m },
                    new OrderItemDto { ProductId = 200, Quantity = 2, UnitPrice = 20m },
                    new OrderItemDto { ProductId = 300, Quantity = 3, UnitPrice = 30m }
                }
            };

            // Act
            var result = mapper.ToEntity(dto);

            // Assert
            Assert.Equal(3, result.Items.Count);
            Assert.Equal(100, result.Items.ElementAt(0).ProductId);
            Assert.Equal(200, result.Items.ElementAt(1).ProductId);
            Assert.Equal(300, result.Items.ElementAt(2).ProductId);
        }
    }
}
