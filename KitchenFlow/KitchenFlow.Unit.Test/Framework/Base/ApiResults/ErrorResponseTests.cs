using Arta.Base.Core.ApiResults;
using System;
using System.Collections.Generic;
using Xunit;

namespace Restaurant.UnitTests
{
    public class ErrorResponseTests
    {
        [Fact]
        public void ErrorResponse_Should_Set_Properties_Correctly()
        {
            // Arrange
            var errorDetail = new ErrorDetail
            {
                Code = "InvalidInput",
                Message = "Input data is not valid",
                Target = "Order",
                Details = new List<ErrorSubDetail>
                {
                    new ErrorSubDetail { Code = "Required", Target = "CustomerId", Message = "CustomerId is required" },
                    new ErrorSubDetail { Code = "OutOfRange", Target = "TableId", Message = "TableId is out of range" }
                },
                InnerError = new InnerError
                {
                    Trace = "Stack trace sample",
                    Context = "RequestId:1234"
                }
            };

            // Act
            var response = new ErrorResponse { Error = errorDetail };

            // Assert
            Assert.NotNull(response.Error);
            Assert.Equal("InvalidInput", response.Error.Code);
            Assert.Equal("Input data is not valid", response.Error.Message);
            Assert.Equal("Order", response.Error.Target);

            Assert.NotNull(response.Error.Details);
            Assert.Equal(2, response.Error.Details.Count);

            Assert.Equal("Required", response.Error.Details[0].Code);
            Assert.Equal("CustomerId", response.Error.Details[0].Target);
            Assert.Equal("CustomerId is required", response.Error.Details[0].Message);

            Assert.Equal("OutOfRange", response.Error.Details[1].Code);
            Assert.Equal("TableId", response.Error.Details[1].Target);
            Assert.Equal("TableId is out of range", response.Error.Details[1].Message);

            Assert.NotNull(response.Error.InnerError);
            Assert.Equal("Stack trace sample", response.Error.InnerError.Trace);
            Assert.Equal("RequestId:1234", response.Error.InnerError.Context);
        }
    }
}
