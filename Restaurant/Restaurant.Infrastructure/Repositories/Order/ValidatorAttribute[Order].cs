using Arta.Base.Core.Exceptions;
using Arta.Base.Core.Validators;
using FluentValidation;
using Restaurant.Domain.Contract.Order;
using Restaurant.Domain.Order;
using Restaurant.Infrastructure.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace Restaurant.Infrastructure.Repositories
{
    public partial class ValidatorAttribute : ValidatorAttributeBase
    {
        public class OrderValidator : AbstractValidator<OrderDto>
        {
            public OrderValidator()
            {
                RuleFor(o => o.CustomerId).GreaterThan(0).WithMessage("Customer ID is obligatory");
                RuleFor(o => o.TableId).GreaterThan(0).WithMessage("Table ID is obligatory");
                RuleFor(o => o.Items).NotEmpty().WithMessage("An Order has to have at least one item");
                RuleForEach(o => o.Items).SetValidator(new OrderItemValidator());
            }
        }
        public class OrderItemValidator : AbstractValidator<OrderItemDto>
        {
            public OrderItemValidator()
            {
                RuleFor(i => i.ProductId).GreaterThan(0).WithMessage("Product ID is obligatory");
                RuleFor(i => i.Quantity).GreaterThan(0).WithMessage("Quantity should be Greater Than 0");
                RuleFor(i => i.UnitPrice).GreaterThan(0).WithMessage("UnitPrice should be Greater Than 0");
            }
        }
        public ValidatorAttribute(ValidatorType_Order validatorType, string helpParameterName) : base(helpParameterName)
        {
            this.Validate = ValidateOrder;
        }

        public bool ValidateOrder(object value)
        {
            OrderValidator _validations = new OrderValidator();

            var result = _validations.Validate((value as OrderDto)!);

            if (!result.IsValid)
                throw new ValidatorExecption("Wrong Order Information ", result);

            return false;
        }
    }
}
