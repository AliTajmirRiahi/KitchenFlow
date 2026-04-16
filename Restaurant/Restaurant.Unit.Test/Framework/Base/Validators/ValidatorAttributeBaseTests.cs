using Arta.Base.Core.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Restaurant.Unit.Test.Framework.Base.Validators
{
    // کلاس Fake برای تست
    public class FakeValidatorAttribute : ValidatorAttributeBase
    {
        public FakeValidatorAttribute(string helpParameterName)
            : base(helpParameterName)
        {
        }
    }

    public class ValidatorAttributeBaseTests
    {
        [Fact]
        public void Validate_Should_Return_True_When_Function_Is_Set()
        {
            // Arrange
            var attribute = new FakeValidatorAttribute("TestParam");
            attribute.Validate = obj => obj is int i && i > 0;

            // Act
            var result = attribute.Validate(5);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Validate_Should_Return_False_When_Function_Is_Set()
        {
            // Arrange
            var attribute = new FakeValidatorAttribute("TestParam");
            attribute.Validate = obj => obj is int i && i > 0;

            // Act
            var result = attribute.Validate(-10);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Validate_Should_Throw_When_Not_Set()
        {
            // Arrange
            var attribute = new FakeValidatorAttribute("TestParam");

            // Act & Assert
            Assert.Throws<NullReferenceException>(() => attribute.Validate("anything"));
        }
    }
}
