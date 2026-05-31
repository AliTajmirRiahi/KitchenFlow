using Arta.Base.Core.Validators;
using Xunit;

namespace Restaurant.Unit.Test.Framework.Base.Validators;

/// <summary>
/// Fake validator attribute used only for unit testing the base class behavior.
/// </summary>
public class FakeValidatorAttribute : ValidatorAttributeBase
{
    public FakeValidatorAttribute(string helpParameterName)
        : base(helpParameterName)
    {
    }

    /// <summary>
    /// Exposes the protected validation logic for unit tests.
    /// </summary>
    public void ExecuteValidation(object value)
    {
        Validate(value);
    }

    /// <summary>
    /// Test-specific implementation of validation logic.
    /// </summary>
    protected override void Validate(object value)
    {
        if (value is int number && number > 0)
            return;

        throw new InvalidOperationException("Validation failed.");
    }
}



/// <summary>
/// Unit tests for FakeValidatorAttribute / ValidatorAttributeBase behavior.
/// </summary>
public class ValidatorAttributeBaseTests
{
    [Fact]
    public void Validate_Should_Not_Throw_When_Input_Is_Valid()
    {
        // Arrange
        var attribute = new FakeValidatorAttribute("TestParam");

        // Act
        var exception = Record.Exception(() => attribute.ExecuteValidation(5));

        // Assert
        Assert.Null(exception);
    }

    [Fact]
    public void Validate_Should_Throw_When_Input_Is_Invalid()
    {
        // Arrange
        var attribute = new FakeValidatorAttribute("TestParam");

        // Act
        var exception = Record.Exception(() => attribute.ExecuteValidation(-10));

        // Assert
        Assert.NotNull(exception);
        Assert.IsType<InvalidOperationException>(exception);
    }

    [Fact]
    public void Validate_Should_Throw_When_Input_Is_Not_Integer()
    {
        // Arrange
        var attribute = new FakeValidatorAttribute("TestParam");

        // Act
        var exception = Record.Exception(() => attribute.ExecuteValidation("anything"));

        // Assert
        Assert.NotNull(exception);
        Assert.IsType<InvalidOperationException>(exception);
    }
}
