using Arta.Base.Core.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Arta.Base.Core.Validators
{
    public abstract partial class ValidatorAttributeBase : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext context)
        {

            if (string.IsNullOrEmpty(_helpParameterName))
                throw new BaseException("Validation helpParameterName can not be empty or null", "ValidatorAttributeError", System.Net.HttpStatusCode.BadRequest);

            if(!context.ActionArguments.ContainsKey(_helpParameterName))
                throw new BaseException("ActionArguments doesen't have " + _helpParameterName, "ValidatorAttributeError", System.Net.HttpStatusCode.BadRequest);

            var value = context.ActionArguments[_helpParameterName];

            if (value == null)
                throw new BaseException("Validation Object can not be null", "ValidatorAttributeError", System.Net.HttpStatusCode.BadRequest);

            if (_validate == null)
                throw new BaseException("Validate Func can not be null", "ValidatorAttributeError", System.Net.HttpStatusCode.BadRequest);

            _validate(value);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
