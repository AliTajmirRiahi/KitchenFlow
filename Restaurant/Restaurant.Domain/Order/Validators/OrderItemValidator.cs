using FluentValidation;
using Restaurant.Domain.Contract.Order;

namespace Restaurant.Domain.Order.Validators;

public class OrderItemValidator : AbstractValidator<OrderItemDto>
{
    public OrderItemValidator()
    {
        RuleFor(i => i.ProductId)
            .GreaterThan(0)
            .WithMessage("Product ID is obligatory");

        RuleFor(i => i.Quantity)
            .GreaterThan(0)
            .WithMessage("Quantity must be greater than zero");

        RuleFor(i => i.UnitPrice)
            .GreaterThan(0)
            .WithMessage("Unit price must be greater than zero");
    }
}

public class OrderItemsValidator : AbstractValidator<IEnumerable<OrderItemDto>>
{
    public OrderItemsValidator()
    {
        // Validate the collection itself
        RuleFor(x => x)
            .NotNull().WithMessage("Order items are required.")
            .NotEmpty().WithMessage("At least one order item is required.");

        // Validate each element in the collection
        RuleForEach(x => x)
            .SetValidator(new OrderItemValidator());
    }
}
