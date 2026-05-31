using System.Net;
using System.Text.Json;
using Arta.Base.Core.ApiResults;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Arta.Base.Core.Exceptions;
using Restaurant.Presentation;

namespace Restaurant.Integration.Test.Framework.Application.Base.Exceptions
{
    public class ExceptionHandlingMiddlewareIntegrationTests
    : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public ExceptionHandlingMiddlewareIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task ValidatorExecption_Should_Be_Handled_By_Middleware()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/test/throw_ValidatorExecption");
            var content = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var result = JsonSerializer.Deserialize<ErrorResponse>(content,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            Assert.NotNull(result);
            Assert.Equal("ValidationError", result!.Error.Code);
            Assert.Equal("Validation failed", result.Error.Message);
            Assert.NotEmpty(result.Error.Details);
        }

        [Fact]
        public async Task BaseException_Should_Be_Handled_By_Middleware()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/test/throw_BaseExecption");
            var content = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var result = JsonSerializer.Deserialize<ErrorResponse>(content,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            Assert.NotNull(result);
            Assert.Equal("Base Execption", result!.Error.Code);
            Assert.Equal("Base Execption", result.Error.Message);
        }

    }
}

