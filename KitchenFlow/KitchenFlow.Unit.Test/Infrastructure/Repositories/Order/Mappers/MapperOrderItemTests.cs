using Xunit;
using KitchenFlow.Domain.Order;
using KitchenFlow.Domain.Order.Mappers;
using KitchenFlow.Domain.Contract.Order;

namespace KitchenFlow.Unit.Test.Infrastructure.Repositories.Order.Mappers
{
    public class MapperOrderItemTests
    {
        private readonly MapperOrderItem _sut;

        public MapperOrderItemTests()
        {
            // System Under Test
            _sut = new MapperOrderItem();
        }

        [Fact]
        public void ToDto_ShouldMapAllPropertiesCorrectly()
        {
            // Arrange - Create a domain entity
            // Assuming OrderItem constructor: (productId, quantity, unitPrice)
            var productId = 101;
            var quantity = 5;
            var unitPrice = 1500.50m;
            var orderItem = new OrderItem(productId, quantity, unitPrice);

            // Act
            var result = _sut.ToDto(orderItem);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(productId, result.ProductId);
            Assert.Equal(quantity, result.Quantity);
            Assert.Equal(unitPrice, result.UnitPrice);
        }

        [Fact]
        public void ToEntity_ShouldCreateOrderItemWithCorrectValues()
        {
            // Arrange - Create a DTO
            var dto = new OrderItemDto
            {
                ProductId = 202,
                Quantity = 10,
                UnitPrice = 3000m
            };

            // Act
            var result = _sut.ToEntity(dto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(dto.ProductId, result.ProductId);
            Assert.Equal(dto.Quantity, result.Quantity);
            Assert.Equal(dto.UnitPrice, result.UnitPrice);
        }

        [Theory]
        [InlineData(1, 1, 0.0)]
        [InlineData(999, 100, 999999.99)]
        public void ToEntity_ShouldHandleVariousValues(int productId, int quantity, decimal unitPrice)
        {
            // Arrange
            var dto = new OrderItemDto
            {
                ProductId = productId,
                Quantity = quantity,
                UnitPrice = unitPrice
            };

            // Act
            var result = _sut.ToEntity(dto);

            // Assert
            Assert.Equal(productId, result.ProductId);
            Assert.Equal(quantity, result.Quantity);
            Assert.Equal(unitPrice, result.UnitPrice);
        }
    }
}
