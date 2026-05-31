using Arta.Base.Core.Exceptions;
using Arta.Base.Core.Validators;
using FluentValidation;

namespace KitchenFlow.Presentation.Validators;

public class ValidatorAttribute : ValidatorAttributeBase
{
    private readonly Type _validatorType;

    public ValidatorAttribute(Type validatorType, string parameterName)
        : base(parameterName)
    {
        _validatorType = validatorType;
    }

    protected override void Validate(object value)
    {
        if (value == null)
            throw new BaseException("Validation Object can not be null", "ValidatorAttributeError", System.Net.HttpStatusCode.BadRequest);

        var instance = Activator.CreateInstance(_validatorType);

        if (instance is not IValidator validator)
            throw new BaseException("Validator can not be null", "ValidatorAttributeError", System.Net.HttpStatusCode.BadRequest);

        var context = new ValidationContext<object>(value);

        var result = validator.Validate(context);

        if (!result.IsValid)
            throw new ValidatorExecption("Validation Error", result);
    }
}