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
        public class TestValidator : AbstractValidator<dynamic>
        {
            public TestValidator()
            {
                
            }
        }
        public ValidatorAttribute(ValidatorType_Test validatorType, string helpParameterName) : base(helpParameterName)
        {
            this.Validate = null; //this is for test
        }
        
    }
}
