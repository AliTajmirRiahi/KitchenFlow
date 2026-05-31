using System.Net;
using System.Text.Json;
using Arta.Base.Core.ApiResults;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Arta.Base.Core.Exceptions;
using KitchenFlow.Presentation;
using System.Net.Http.Json;

namespace KitchenFlow.Integration.Test.Presentation.Validators
{
    public class ValidatorAttributeTests
    : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public ValidatorAttributeTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
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
            Assert.Equal("Validator can not be null", result.Error.Message);
        }
    }
}

