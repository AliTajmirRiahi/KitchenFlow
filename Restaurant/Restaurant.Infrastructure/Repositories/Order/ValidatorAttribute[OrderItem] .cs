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
    //public partial class ValidatorAttribute : ValidatorAttributeBase
    //{
    //    public class OrderItemValidator : AbstractValidator<OrderItem>
    //    {
    //        public OrderItemValidator()
    //        {
    //        }
    //    }
    //    public ValidatorAttribute(ValidatorType_OrderItem validatorType, string helpParameterName) : base(helpParameterName)
    //    {
    //        this.Validate = ValidateOrderItem;
    //    }

    //    public bool ValidateOrderItem(object value)
    //    {
    //        OrderValidator _validations = new OrderValidator();

    //        var result = _validations.Validate((value as OrderDto)!);

    //        if (!result.IsValid)
    //            throw new ValidatorExecption("سفارش باید حداقل شامل یک آیتم باشد", result);

    //        return false;
    //    }
    //}
}
