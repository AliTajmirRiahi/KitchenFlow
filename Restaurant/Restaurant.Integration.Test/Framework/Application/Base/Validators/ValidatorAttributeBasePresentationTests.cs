using System.Net;
using System.Text.Json;
using Arta.Base.Core.ApiResults;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Arta.Base.Core.Exceptions;
using Restaurant.Presentation;
using System.Net.Http.Json;

namespace Restaurant.Integration.Test.Framework.Application.Base.Exceptions
{
    public class ValidatorAttributeBasePresentationTests
    : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public ValidatorAttributeBasePresentationTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Validator_When_Helper_Is_Null_Should_Throw()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/test/Validation_Helper_Is_Null");
            var content = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var result = JsonSerializer.Deserialize<ErrorResponse>(content,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            Assert.NotNull(result);
            Assert.Equal("ValidatorAttributeError", result!.Error.Code);
            Assert.Equal("Validation helpParameterName can not be empty or null", result.Error.Message);
        }

        [Fact]
        public async Task Validator_When_ActionArguments_Not_Have_Helper_Should_Throw()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/test/Validator_When_ActionArguments_Not_Have_Helper");
            var content = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var result = JsonSerializer.Deserialize<ErrorResponse>(content,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            Assert.NotNull(result);
            Assert.Equal("ValidatorAttributeError", result!.Error.Code);
            Assert.Equal("ActionArguments doesen't have obj", result.Error.Message);
        }

        [Fact]
        public async Task Validator_When_Helper_Value_Is_Null_Should_Throw()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.PostAsJsonAsync("/test/Validation_Helper_Value_Is_Null", (object)null!);
            var content = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var result = JsonSerializer.Deserialize<ErrorResponse>(content,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            Assert.NotNull(result);
            Assert.Equal("ValidatorAttributeError", result!.Error.Code);
            Assert.Equal("Validation Object can not be null", result.Error.Message);
        }

        [Fact]
        public async Task Validator_When_Validate_Func_Is_Null_Should_Throw()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.PostAsJsonAsync("/test/Validation_Helper_Value_Is_Null", "object");
            var content = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var result = JsonSerializer.Deserialize<ErrorResponse>(content,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            Assert.NotNull(result);
            Assert.Equal("ValidatorAttributeError", result!.Error.Code);
            Assert.Equal("Validate Func can not be null", result.Error.Message);
        }
    }
}

