using Moq;
using Restaurant.Application;
using Restaurant.Domain.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Unit.Test.Application
{
    public class OrderServiceTests
    {
        [Fact]
        public async Task AddAsync_Should_Call_Repository_And_Return_Id()
        {
            // Arrange
            var order = new Order(1, 2);
            var expectedId = Guid.NewGuid();

            var repositoryMock = new Mock<IOrderRepository>();
            repositoryMock
                .Setup(r => r.AddAsync(order))
                .ReturnsAsync(expectedId);

            var service = new OrderService(repositoryMock.Object);

            // Act
            var result = await service.AddAsync(order);

            // Assert
            Assert.Equal(expectedId, result);
            repositoryMock.Verify(r => r.AddAsync(order), Times.Once);
        }
    }
}
