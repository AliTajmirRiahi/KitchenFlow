using Arta.Base.Core.ApiResults;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Restaurant.Application;
using Restaurant.Domain.Contract.Order;
using Restaurant.Domain.Order;
using Restaurant.Presentation.Configs.ApiResults;
using Restaurant.Presentation.Controllers.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Unit.Test.Presentation.Controllers
{
    public class OrderControllerTests
    {
        [Fact]
        public async Task AddOrder_Should_Call_Service_And_Return_OrderId()
        {
            // Arrange
            var orderDto = new OrderDto
            {
                CustomerId = 1,
                TableId = 2
            };

            var order = new Order(orderDto.CustomerId, orderDto.TableId);
            var expectedOrderId = Guid.NewGuid();

            var serviceMock = new Mock<IOrderService>();
            serviceMock.Setup(s => s.AddAsync(It.IsAny<Order>()))
                .ReturnsAsync(expectedOrderId);

            var mapperMock = new Mock<IMapperOrder>();
            mapperMock.Setup(m => m.CustomMapOrderDtoToOrder(orderDto))
                .Returns(order);

            var controller = new OrderController(serviceMock.Object, mapperMock.Object);

            // Act
            var result = await controller.AddOrder(orderDto) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            var apiResult = result.Value as ApiResult<GuidEntity>;
            Assert.NotNull(apiResult);
            Assert.Equal(expectedOrderId, apiResult.Payload!.Id);

            serviceMock.Verify(s => s.AddAsync(order), Times.Once);
            mapperMock.Verify(m => m.CustomMapOrderDtoToOrder(orderDto), Times.Once);
        }
    }
}
