using FluentValidation;
using Microgroove.CustomerApi.Function.Models.Request;
using System;

namespace Microgroove.CustomerApi.Function.Validators
{
    public class CreateCustomerRequestValidator : AbstractValidator<CreateCustomerRequest>
    {
        public CreateCustomerRequestValidator()
        {
            RuleFor(customer => customer.FullName).NotEmpty().WithMessage("Please specify a valid FullName.");
            RuleFor(customer => customer.DateOfBirth).Must(ValidateDateOfBirth).WithMessage("Please specify a valid DateOfBirth.");
        }

        private bool ValidateDateOfBirth(string dateOfBirth)
        {
            return DateOnly.TryParseExact(dateOfBirth, "yyyyMMdd", out var d) && d < DateOnly.FromDateTime(DateTime.Now);
        }
    }
}
