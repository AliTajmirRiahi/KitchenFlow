using Arta.Base.Core.Exceptions;
using Moq;
using KitchenFlow.Application;
using KitchenFlow.Domain.Contract.Order;
using KitchenFlow.Domain.Order;
using KitchenFlow.Domain.Order.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitchenFlow.Unit.Test.Application
{
    public class OrderServiceTests
    {
        [Fact]
        public async Task AddAsync_Should_Call_Repository_And_Return_Id()
        {
            // Arrange
            var order = new Order(1, 2);
            var orderDto = new OrderDto()
            {
                CustomerId = order.CustomerId,
                TableId = order.TableId,
            };
            var expectedId = Guid.NewGuid();

            var repositoryMock = new Mock<IOrderRepository>();
            repositoryMock
                .Setup(r => r.AddAsync(order))
                .ReturnsAsync(expectedId);

            var mapperOrderMock = new Mock<IMapperOrder>();
            mapperOrderMock.Setup(r => r.ToEntity(orderDto)).Returns(order);

            var service = new OrderService(repositoryMock.Object, mapperOrderMock.Object);

            // Act
            var result = await service.AddAsync(orderDto);

            // Assert
            Assert.Equal(expectedId, result);
            repositoryMock.Verify(r => r.AddAsync(order), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_Should_Update_And_Return_Dto()
        {
            // Arrange
            var order = new Order(1, 2);
            var orderId = order.Id;

            var updateDto = new UpdateOrderDto
            {
                TableId = 3,
            };

            var repositoryMock = new Mock<IOrderRepository>();
            repositoryMock
                .Setup(r => r.GetByIdAsync(orderId))
                .ReturnsAsync(order);

            repositoryMock
                .Setup(r => r.UpdateAsync(It.IsAny<Order>()))
                .Returns(Task.CompletedTask);

            var mapperMock = new Mock<IMapperOrder>();
            mapperMock
                .Setup(m => m.ToDto(It.IsAny<Order>()))
                .Returns<Order>(o => new OrderDto { TableId = o.TableId });

            var service = new OrderService(repositoryMock.Object, mapperMock.Object);

            // Act
            var result = await service.UpdateAsync(orderId, updateDto);

            // Assert
            Assert.Equal(updateDto.TableId, result.TableId);

            repositoryMock.Verify(r => r.UpdateAsync(It.Is<Order>(o => o.TableId == 3)), Times.Once);
            repositoryMock.Verify(r => r.GetByIdAsync(orderId), Times.Once);
            repositoryMock.Verify(r => r.UpdateAsync(order), Times.Once);
            mapperMock.Verify(m => m.ToDto(order), Times.Once);
        }

        [Fact]
        public async Task AddItemsAsync_Should_Add_Items_And_Return_Dto()
        {
            // Arrange
            var order = new Order(1, 2);
            var orderId = order.Id;

            var items = new List<OrderItemDto>
            {
                new OrderItemDto
                {
                    ProductId = 10,
                    Quantity = 2,
                    UnitPrice = 100
                },
                new OrderItemDto
                {
                    ProductId = 20,
                    Quantity = 1,
                    UnitPrice = 200
                }
            };

            var expectedDto = new OrderDto
            {
                CustomerId = order.CustomerId,
                TableId = order.TableId,
                Items = new List<OrderItemDto>(items)
            };

            var repositoryMock = new Mock<IOrderRepository>();
            repositoryMock
                .Setup(r => r.GetByIdAsync(orderId))
                .ReturnsAsync(order);

            repositoryMock
                .Setup(r => r.UpdateAsync(order))
                .Returns(Task.CompletedTask);

            var mapperMock = new Mock<IMapperOrder>();
            mapperMock
                .Setup(m => m.ToDto(order))
                .Returns(expectedDto);

            var service = new OrderService(repositoryMock.Object, mapperMock.Object);

            // Act
            var result = await service.AddItemsAsync(orderId, items);

            // Assert
            Assert.Equal(expectedDto.CustomerId, result.CustomerId);
            Assert.Equal(expectedDto.TableId, result.TableId);
            Assert.Equal(expectedDto.Items.Count, result.Items.Count);
            Assert.Equal(expectedDto.Items[0].ProductId, result.Items[0].ProductId);
            Assert.Equal(expectedDto.Items[0].Quantity, result.Items[0].Quantity);
            Assert.Equal(expectedDto.Items[0].UnitPrice, result.Items[0].UnitPrice);

            repositoryMock.Verify(r => r.GetByIdAsync(orderId), Times.Once);
            repositoryMock.Verify(r => r.UpdateAsync(order), Times.Once);
            mapperMock.Verify(m => m.ToDto(order), Times.Once);
        }

        [Fact]
        public async Task GetAsync_Should_Return_Dto()
        {
            // Arrange
            var order = new Order(1, 2);
            var orderId = order.Id;

            var expectedDto = new OrderDto
            {
                CustomerId = order.CustomerId,
                TableId = order.TableId,
                Items = new List<OrderItemDto>()
            };

            var repositoryMock = new Mock<IOrderRepository>();
            repositoryMock
                .Setup(r => r.GetByIdAsync(orderId))
                .ReturnsAsync(order);

            var mapperMock = new Mock<IMapperOrder>();
            mapperMock
                .Setup(m => m.ToDto(order))
                .Returns(expectedDto);

            var service = new OrderService(repositoryMock.Object, mapperMock.Object);

            // Act
            var result = await service.GetAsync(orderId);

            // Assert
            Assert.Equal(expectedDto.CustomerId, result.CustomerId);
            Assert.Equal(expectedDto.TableId, result.TableId);
            Assert.NotNull(result.Items);

            repositoryMock.Verify(r => r.GetByIdAsync(orderId), Times.Once);
            mapperMock.Verify(m => m.ToDto(order), Times.Once);
        }

        [Fact]
        public async Task AcceptOrderAsync_Should_Update_Order_And_Return_Dto()
        {
            // Arrange
            var order = new Order(1, 2);
            var orderId = order.Id;

            var expectedDto = new OrderDto
            {
                CustomerId = order.CustomerId,
                TableId = order.TableId,
                Items = new List<OrderItemDto>()
            };

            var repositoryMock = new Mock<IOrderRepository>();
            repositoryMock
                .Setup(r => r.GetByIdAsync(orderId))
                .ReturnsAsync(order);

            repositoryMock
                .Setup(r => r.UpdateAsync(order))
                .Returns(Task.CompletedTask);

            var mapperMock = new Mock<IMapperOrder>();
            mapperMock
                .Setup(m => m.ToDto(order))
                .Returns(expectedDto);

            var service = new OrderService(repositoryMock.Object, mapperMock.Object);

            // Act
            var result = await service.AcceptOrderAsync(orderId);

            // Assert
            Assert.Equal(expectedDto.CustomerId, result.CustomerId);
            Assert.Equal(expectedDto.TableId, result.TableId);

            repositoryMock.Verify(r => r.GetByIdAsync(orderId), Times.Once);
            repositoryMock.Verify(r => r.UpdateAsync(order), Times.Once);
            mapperMock.Verify(m => m.ToDto(order), Times.Once);
        }

        [Fact]
        public async Task StartPreparationAsync_Should_Update_Order_And_Return_Dto()
        {
            // Arrange
            var order = new Order(1, 2);
            var orderId = order.Id;

            order.Accept();

            var expectedDto = new OrderDto
            {
                CustomerId = order.CustomerId,
                TableId = order.TableId,
                Items = new List<OrderItemDto>()
            };

            var repositoryMock = new Mock<IOrderRepository>();
            repositoryMock
                .Setup(r => r.GetByIdAsync(orderId))
                .ReturnsAsync(order);

            repositoryMock
                .Setup(r => r.UpdateAsync(order))
                .Returns(Task.CompletedTask);

            var mapperMock = new Mock<IMapperOrder>();
            mapperMock
                .Setup(m => m.ToDto(order))
                .Returns(expectedDto);

            var service = new OrderService(repositoryMock.Object, mapperMock.Object);

            // Act
            var result = await service.StartPreparationAsync(orderId);

            // Assert
            Assert.Equal(expectedDto.CustomerId, result.CustomerId);
            Assert.Equal(expectedDto.TableId, result.TableId);

            repositoryMock.Verify(r => r.GetByIdAsync(orderId), Times.Once);
            repositoryMock.Verify(r => r.UpdateAsync(order), Times.Once);
            mapperMock.Verify(m => m.ToDto(order), Times.Once);
        }

        [Fact]
        public async Task FinishPreparationAsync_Should_Update_Order_And_Return_Dto()
        {
            // Arrange
            var order = new Order(1, 2);
            var orderId = order.Id;

            order.Accept();
            order.StartPreparation();

            var expectedDto = new OrderDto
            {
                CustomerId = order.CustomerId,
                TableId = order.TableId,
                Items = new List<OrderItemDto>()
            };

            var repositoryMock = new Mock<IOrderRepository>();
            repositoryMock
                .Setup(r => r.GetByIdAsync(orderId))
                .ReturnsAsync(order);

            repositoryMock
                .Setup(r => r.UpdateAsync(order))
                .Returns(Task.CompletedTask);

            var mapperMock = new Mock<IMapperOrder>();
            mapperMock
                .Setup(m => m.ToDto(order))
                .Returns(expectedDto);

            var service = new OrderService(repositoryMock.Object, mapperMock.Object);

            // Act
            var result = await service.FinishPreparationAsync(orderId);

            // Assert
            Assert.Equal(expectedDto.CustomerId, result.CustomerId);
            Assert.Equal(expectedDto.TableId, result.TableId);

            repositoryMock.Verify(r => r.GetByIdAsync(orderId), Times.Once);
            repositoryMock.Verify(r => r.UpdateAsync(order), Times.Once);
            mapperMock.Verify(m => m.ToDto(order), Times.Once);
        }

        [Fact]
        public async Task ServeAsync_Should_Update_Order_And_Return_Dto()
        {
            // Arrange
            var order = new Order(1, 2);
            var orderId = order.Id;

            order.Accept();
            order.StartPreparation();
            order.FinishPreparation();

            var expectedDto = new OrderDto
            {
                CustomerId = order.CustomerId,
                TableId = order.TableId,
                Items = new List<OrderItemDto>()
            };

            var repositoryMock = new Mock<IOrderRepository>();
            repositoryMock
                .Setup(r => r.GetByIdAsync(orderId))
                .ReturnsAsync(order);

            repositoryMock
                .Setup(r => r.UpdateAsync(order))
                .Returns(Task.CompletedTask);

            var mapperMock = new Mock<IMapperOrder>();
            mapperMock
                .Setup(m => m.ToDto(order))
                .Returns(expectedDto);

            var service = new OrderService(repositoryMock.Object, mapperMock.Object);

            // Act
            var result = await service.ServeAsync(orderId);

            // Assert
            Assert.Equal(expectedDto.CustomerId, result.CustomerId);
            Assert.Equal(expectedDto.TableId, result.TableId);

            repositoryMock.Verify(r => r.GetByIdAsync(orderId), Times.Once);
            repositoryMock.Verify(r => r.UpdateAsync(order), Times.Once);
            mapperMock.Verify(m => m.ToDto(order), Times.Once);
        }

        [Fact]
        public async Task CloseAsync_Should_Update_Order_And_Return_Dto()
        {
            // Arrange
            var order = new Order(1, 2);

            order.Accept();
            order.StartPreparation();
            order.FinishPreparation();
            order.Serve();

            var orderId = order.Id;

            var expectedDto = new OrderDto
            {
                CustomerId = order.CustomerId,
                TableId = order.TableId,
                Items = new List<OrderItemDto>()
            };

            var repositoryMock = new Mock<IOrderRepository>();
            repositoryMock
                .Setup(r => r.GetByIdAsync(orderId))
                .ReturnsAsync(order);

            repositoryMock
                .Setup(r => r.UpdateAsync(order))
                .Returns(Task.CompletedTask);

            var mapperMock = new Mock<IMapperOrder>();
            mapperMock
                .Setup(m => m.ToDto(order))
                .Returns(expectedDto);

            var service = new OrderService(repositoryMock.Object, mapperMock.Object);

            // Act
            var result = await service.CloseAsync(orderId);

            // Assert
            Assert.Equal(expectedDto.CustomerId, result.CustomerId);
            Assert.Equal(expectedDto.TableId, result.TableId);

            repositoryMock.Verify(r => r.GetByIdAsync(orderId), Times.Once);
            repositoryMock.Verify(r => r.UpdateAsync(order), Times.Once);
            mapperMock.Verify(m => m.ToDto(order), Times.Once);
        }

        [Fact]
        public async Task CancelAsync_Should_Update_Order_And_Return_Dto()
        {
            // Arrange
            var order = new Order(1, 2);
            var orderId = order.Id;

            var expectedDto = new OrderDto
            {
                CustomerId = order.CustomerId,
                TableId = order.TableId,
                Items = new List<OrderItemDto>()
            };

            var repositoryMock = new Mock<IOrderRepository>();
            repositoryMock
                .Setup(r => r.GetByIdAsync(orderId))
                .ReturnsAsync(order);

            repositoryMock
                .Setup(r => r.UpdateAsync(order))
                .Returns(Task.CompletedTask);

            var mapperMock = new Mock<IMapperOrder>();
            mapperMock
                .Setup(m => m.ToDto(order))
                .Returns(expectedDto);

            var service = new OrderService(repositoryMock.Object, mapperMock.Object);

            // Act
            var result = await service.CancelAsync(orderId);

            // Assert
            Assert.Equal(expectedDto.CustomerId, result.CustomerId);
            Assert.Equal(expectedDto.TableId, result.TableId);

            repositoryMock.Verify(r => r.GetByIdAsync(orderId), Times.Once);
            repositoryMock.Verify(r => r.UpdateAsync(order), Times.Once);
            mapperMock.Verify(m => m.ToDto(order), Times.Once);
        }

        [Fact]
        public async Task GetAsync_Should_Throw_NotFoundException_When_Order_Not_Found()
        {
            // Arrange
            var orderId = new Guid();

            var repositoryMock = new Mock<IOrderRepository>();
            repositoryMock
                .Setup(r => r.GetByIdAsync(orderId))
                .ReturnsAsync(null as Order);

            var mapperMock = new Mock<IMapperOrder>();

            var service = new OrderService(repositoryMock.Object, mapperMock.Object);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => service.GetAsync(orderId));
        }

    }
}
