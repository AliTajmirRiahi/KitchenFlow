using Restaurant.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Domain.Order;
using Arta.Base.Core.ApiResults;
using Arta.Base.Core.Exceptions;
using Restaurant.Infrastructure.Commons;
using System;

namespace Restaurant.Presentation.Controllers.Tests
{
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class TestController : PresentationControllerBase
    {
        public TestController()
        {
        }

        [HttpGet("throw_ValidatorExecption")]
        public IActionResult throw_ValidatorExecption()
        {
            throw new ValidatorExecption(
                "Validation failed",
                new FluentValidation.Results.ValidationResult(new[]
                {
                new FluentValidation.Results.ValidationFailure("Name", "Name is required"),
                new FluentValidation.Results.ValidationFailure("Age", "Age must be greater than 18"),
                })
            );
        }
        [HttpGet("throw_BaseExecption")]
        public IActionResult throw_BaseExecption()
        {
            throw new BaseException("Base Execption", "Base Execption", System.Net.HttpStatusCode.BadRequest);
        }

        [HttpGet("Validation_Helper_Is_Null")]
        [Validator(ValidatorType_Test.Basic, null!)]
        public IActionResult Validation_Helper_Is_Null()
        {
            return Ok();
        }

        [HttpGet("Validator_When_ActionArguments_Not_Have_Helper")]
        [Validator(ValidatorType_Test.Basic, "obj")]
        public IActionResult Validator_When_ActionArguments_Not_Have_Helper()
        {
            return Ok();
        }

        [HttpPost("Validation_Helper_Value_Is_Null")]
        [Validator(ValidatorType_Test.Basic, "obj")]
        public IActionResult Validation_Helper_Value_Is_Null(object? obj)
        {
            return Ok();
        }

        [HttpPost("Validation_Validate_Func_Is_Null")]
        [Validator(ValidatorType_Test.Basic, "obj")]
        public IActionResult Validation_Validate_Func_Is_Null(object obj)
        {
            return Ok();
        }
    }
}
