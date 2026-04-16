using System;
using System.Linq;
using Xunit;
using Restaurant.Domain.Order;
using static Arta.Domain.Core.Commons.Enums.Enums;

namespace Restaurant.Tests.Domain
{
    public class OrderTests
    {
        [Fact]
        public void NewOrder_ShouldHaveCreatedStatus()
        {
            // Arrange
            int customerId = 1;
            int tableId = 5;

            // Act
            var order = new Order(customerId, tableId);

            // Assert
            Assert.Equal(OrderStatus.Created, order.Status);
            Assert.Equal(customerId, order.CustomerId);
            Assert.Equal(tableId, order.TableId);
            Assert.Empty(order.Items);
        }

        [Fact]
        public void AddItem_ShouldAddOrderItem()
        {
            // Arrange
            var order = new Order(1, 1);

            // Act
            order.AddItem(productId: 100, quantity: 2, unitPrice: 50m);

            // Assert
            Assert.Single(order.Items);
            var item = order.Items.First();
            Assert.Equal(100, item.ProductId);
            Assert.Equal(2, item.Quantity);
            Assert.Equal(50m, item.UnitPrice);
            Assert.Equal(100m, item.Total);
        }

        [Fact]
        public void GetTotalAmount_ShouldReturnSumOfItems()
        {
            // Arrange
            var order = new Order(1, 1);
            order.AddItem(1, 2, 10m);
            order.AddItem(2, 3, 20m);

            // Act
            var total = order.GetTotalAmount();

            // Assert
            Assert.Equal(2 * 10 + 3 * 20, total);
        }

        [Fact]
        public void Accept_ShouldChangeStatusToConfirmed()
        {
            // Arrange
            var order = new Order(1, 1);

            // Act
            order.Accept();

            // Assert
            Assert.Equal(OrderStatus.Confirmed, order.Status);
        }

        [Fact]
        public void GoNext_ShouldMoveToNextState()
        {
            // Arrange
            var order = new Order(1, 1);

            // Act
            order.GoNextState();

            // Assert
            // وضعیت بعد از Created، Confirmed است
            Assert.Equal(OrderStatus.Confirmed, order.Status);
        }

        [Fact]
        public void AddingItemAfterConfirmed_ShouldThrow()
        {
            // Arrange
            var order = new Order(1, 1);
            order.Accept();
            order.StartPreparation();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() =>
                order.AddItem(1, 1, 10m));
        }

        [Fact]
        public void NoNextStateAfterClose_ShouldThrowWhenGoNext()
        {
            // Arrange
            var order = new Order(1, 1);
            order.Accept();
            order.StartPreparation();
            order.FinishPreparation();
            order.Serve();
            order.Close();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => order.GoNextState());
        }

        [Theory]
        // CurrentState, Trigger, ExpectedState
        [InlineData(OrderStatus.Created, OrderTrigger.Accept, OrderStatus.Confirmed)]
        [InlineData(OrderStatus.Created, OrderTrigger.Cancel, OrderStatus.Cancelled)]
        [InlineData(OrderStatus.Confirmed, OrderTrigger.StartPrep, OrderStatus.InPreparation)]
        [InlineData(OrderStatus.InPreparation, OrderTrigger.FinishPrep, OrderStatus.Ready)]
        [InlineData(OrderStatus.Ready, OrderTrigger.Serve, OrderStatus.Delivered)]
        [InlineData(OrderStatus.Delivered, OrderTrigger.Close, OrderStatus.Completed)]
        public void FireTrigger_ShouldChangeStatusCorrectly(
            OrderStatus currentState,
            OrderTrigger trigger,
            OrderStatus expectedState)
        {
            // Arrange
            var order = new Order(1, 1);
            order.InitializeStateMachine(currentState);

            // Act
            switch (trigger)
            {
                case OrderTrigger.Accept: order.Accept(); break;
                case OrderTrigger.Cancel: order.Cancel(); break;
                case OrderTrigger.StartPrep: order.StartPreparation(); break;
                case OrderTrigger.FinishPrep: order.FinishPreparation(); break;
                case OrderTrigger.Serve: order.Serve(); break;
                case OrderTrigger.Close: order.Close(); break;
                default: throw new NotSupportedException();
            }

            // Assert
            Assert.Equal(expectedState, order.Status);
        }

        [Fact]
        public void GoNext_ShouldMoveThroughStatesAutomatically()
        {
            var order = new Order(1, 1);

            // مسیر کامل از Created تا Completed
            var expectedStates = new[]
            {
                OrderStatus.Confirmed,
                OrderStatus.InPreparation,
                OrderStatus.Ready,
                OrderStatus.Delivered,
                OrderStatus.Completed
            };

            foreach (var expected in expectedStates)
            {
                order.GoNextState();
                Assert.Equal(expected, order.Status);
            }

            // بعد از Completed نباید حالت بعدی داشته باشیم
            Assert.Throws<InvalidOperationException>(() => order.GoNextState());
        }
    }
}
