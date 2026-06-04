using FluentValidation.TestHelper;
using KitchenFlow.Domain.Contract.Order;
using KitchenFlow.Domain.Order.Validators;
using Xunit;

namespace KitchenFlow.Unit.Test.Domain.Order.Validators
{
    /// <summary>
    /// Unit tests for OrderBasicValidator.
    /// Validates all business rules defined for OrderDto.
    /// </summary>
    public class OrderBasicValidatorTests
    {
        private readonly OrderBasicValidator _validator = new();

        #region CustomerId

        [Fact]
        public void Should_Have_Error_When_CustomerId_Is_Zero()
        {
            // Arrange
            var model = CreateValidOrder();
            model.CustomerId = 0;

            // Act
            var result = _validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(o => o.CustomerId)
                  .WithErrorMessage("Customer ID is obligatory");
        }

        [Fact]
        public void Should_Not_Have_Error_When_CustomerId_Is_Greater_Than_Zero()
        {
            // Arrange
            var model = CreateValidOrder();
            model.CustomerId = 1;

            // Act
            var result = _validator.TestValidate(model);

            // Assert
            result.ShouldNotHaveValidationErrorFor(o => o.CustomerId);
        }

        #endregion

        #region TableId

        [Fact]
        public void Should_Have_Error_When_TableId_Is_Zero()
        {
            // Arrange
            var model = CreateValidOrder();
            model.TableId = 0;

            // Act
            var result = _validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(o => o.TableId)
                  .WithErrorMessage("Table ID is obligatory");
        }

        [Fact]
        public void Should_Not_Have_Error_When_TableId_Is_Greater_Than_Zero()
        {
            // Arrange
            var model = CreateValidOrder();
            model.TableId = 2;

            // Act
            var result = _validator.TestValidate(model);

            // Assert
            result.ShouldNotHaveValidationErrorFor(o => o.TableId);
        }

        #endregion

        #region Items

        [Fact]
        public void Should_Have_Error_When_Items_Is_Empty()
        {
            // Arrange
            var model = CreateValidOrder();
            model.Items.Clear();

            // Act
            var result = _validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(o => o.Items)
                  .WithErrorMessage("Order must contain at least one item");
        }

        [Fact]
        public void Should_Not_Have_Error_When_Items_Is_Not_Empty()
        {
            // Arrange
            var model = CreateValidOrder();

            // Act
            var result = _validator.TestValidate(model);

            // Assert
            result.ShouldNotHaveValidationErrorFor(o => o.Items);
        }

        #endregion

        #region OrderItemValidator

        [Fact]
        public void Should_Have_Error_When_Any_OrderItem_Is_Invalid()
        {
            // Arrange
            var model = CreateValidOrder();
            model.Items[0].Quantity = 0; // Invalid according to OrderItemValidator

            // Act
            var result = _validator.Validate(model);

            // Assert
            Assert.False(result.IsValid);
            Assert.NotEmpty(result.Errors);
        }

        [Fact]
        public void Should_Not_Have_Error_When_All_OrderItems_Are_Valid()
        {
            // Arrange
            var model = CreateValidOrder();

            // Act
            var result = _validator.TestValidate(model);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Creates a fully valid OrderDto instance.
        /// This method is used to simplify test arrangements.
        /// </summary>
        private static OrderDto CreateValidOrder()
        {
            return new OrderDto
            {
                CustomerId = 1,
                TableId = 1,
                Items =
                {
                    new OrderItemDto
                    {
                        ProductId = 10,
                        Quantity = 2,
                        UnitPrice = 100
                    }
                }
            };
        }

        #endregion
    }
}
