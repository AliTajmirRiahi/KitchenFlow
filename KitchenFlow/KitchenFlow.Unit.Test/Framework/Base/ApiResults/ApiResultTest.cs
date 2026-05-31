using Arta.Base.Core.ApiResults;
using System;
using System.Collections.Generic;
using Xunit;

namespace KitchenFlow.Unit.Test.Framework.Base.ApiResults
{
    public class ApiResultTests
    {
        [Fact]
        public void Ok_Should_Set_Success_True_And_Payload()
        {
            // Arrange
            var payload = new { Id = 1, Name = "Test" };

            // Act
            var result = ApiResult<object>.Ok(payload);

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Payload);
            Assert.Equal(payload.Id, ((dynamic)result.Payload).Id);
            Assert.Equal(payload.Name, ((dynamic)result.Payload).Name);
            Assert.Null(result.TotalCount);
            Assert.Null(result.Page);
            Assert.Null(result.PageSize);
            Assert.Null(result.Warnings);
        }

        [Fact]
        public void Ok_Should_Set_Pagination_Values_When_Provided()
        {
            // Arrange
            var payload = new List<int> { 1, 2, 3 };

            // Act
            var result = ApiResult<List<int>>.Ok(payload, totalCount: 100, page: 2, pageSize: 10);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(payload, result.Payload);
            Assert.Equal(100, result.TotalCount);
            Assert.Equal(2, result.Page);
            Assert.Equal(10, result.PageSize);
        }

        [Fact]
        public void Ok_Should_Set_Warnings_When_Provided()
        {
            // Arrange
            var payload = "data";
            var warnings = new List<string> { "Low stock", "Delayed response" };

            // Act
            var result = ApiResult<string>.Ok(payload, warnings: warnings);

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Warnings);
            Assert.Contains("Low stock", result.Warnings);
            Assert.Contains("Delayed response", result.Warnings);
        }

        [Fact]
        public void Timestamp_Should_Be_Close_To_UtcNow()
        {
            // Act
            var before = DateTime.UtcNow;
            var result = ApiResult<string>.Ok("test");
            var after = DateTime.UtcNow;

            // Assert
            Assert.True(result.Timestamp >= before && result.Timestamp <= after,
                $"Timestamp {result.Timestamp} was not between {before} and {after}");
        }
    }
}
