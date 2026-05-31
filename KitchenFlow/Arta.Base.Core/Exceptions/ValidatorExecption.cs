using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Results;

namespace Arta.Base.Core.Exceptions
{
    public class ValidatorExecption : BaseException
    {
        public ValidationResult ValidationResult { get; }

        public ValidatorExecption(string message, ValidationResult validationResult)
            : base(message, "ValidationError", HttpStatusCode.BadRequest)
        {
            ValidationResult = validationResult;
        }
    }
}
