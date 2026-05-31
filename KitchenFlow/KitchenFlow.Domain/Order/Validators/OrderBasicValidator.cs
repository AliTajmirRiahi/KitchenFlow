using FluentValidation;
using KitchenFlow.Domain.Contract.Order;

namespace KitchenFlow.Domain.Order.Validators;


public class OrderBasicValidator : AbstractValidator<OrderDto>
{
    public OrderBasicValidator()
    {
        RuleFor(o => o.CustomerId)
            .GreaterThan(0)
            .WithMessage("Customer ID is obligatory");

        RuleFor(o => o.TableId)
            .GreaterThan(0)
            .WithMessage("Table ID is obligatory");

        RuleFor(o => o.Items)
            .NotEmpty()
            .WithMessage("Order must contain at least one item");

        RuleForEach(o => o.Items)
            .SetValidator(new OrderItemValidator());
    }
}

