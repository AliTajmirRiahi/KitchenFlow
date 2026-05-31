using Arta.Base.Core.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Arta.Base.Core.Validators;

[AttributeUsage(AttributeTargets.Method)]
public abstract class ValidatorAttributeBase : ActionFilterAttribute
{
    protected readonly string ParameterName;

    protected ValidatorAttributeBase(string parameterName)
    {
        ParameterName = parameterName;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (string.IsNullOrEmpty(ParameterName))
            throw new BaseException("Validation ParameterName can not be empty or null", "ValidatorAttributeError", System.Net.HttpStatusCode.BadRequest);

        if (!context.ActionArguments.ContainsKey(ParameterName))
            throw new BaseException("ActionArguments doesen't have " + ParameterName, "ValidatorAttributeError", System.Net.HttpStatusCode.BadRequest);

        if (!context.ActionArguments.TryGetValue(ParameterName, out var value))
            return;

        Validate(value!);
    }

    protected abstract void Validate(object value);
}

