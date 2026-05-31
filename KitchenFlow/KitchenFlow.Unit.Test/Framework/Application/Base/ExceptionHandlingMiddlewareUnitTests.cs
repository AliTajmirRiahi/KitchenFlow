using System.Net;
using System.Text.Json;
using Arta.Base.Core.ApiResults;
using Arta.Base.Core.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

namespace KitchenFlow.Unit.Test.Framework.Application.Base
{
    public class ExceptionHandlingMiddlewareUnitTests
    {
        [Fact]
        public async Task Middleware_Should_Handle_ValidationException_And_Return_BadRequest()
        {
            // Arrange
            var validationResult = new FluentValidation.Results.ValidationResult(
                new List<FluentValidation.Results.ValidationFailure>
                {
                new("Email", "Email is required"),
                new("Password", "Password is too short")
                });

            var middleware = new ExceptionHandlingMiddleware(_ =>
                throw new ValidatorExecption("Validation failed", validationResult));

            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();

            // Act
            await middleware.InvokeAsync(context);

            // Assert
            Assert.Equal((int)HttpStatusCode.BadRequest, context.Response.StatusCode);

            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var responseBody = await new StreamReader(context.Response.Body).ReadToEndAsync();

            var result = JsonSerializer.Deserialize<ErrorResponse>(responseBody,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            Assert.NotNull(result);
            Assert.Equal("ValidationError", result!.Error.Code);
            Assert.Equal(2, result.Error.Details.Count);
            Assert.Equal("Email", result.Error.Details[0].Target);
        }
    }
}
