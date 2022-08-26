using FluentValidation;
using FluentValidation.Results;
using System.Collections.Generic;

namespace validations
{
    public static class ValidationExtensions
    {
        public static void AddFailures<T>(this ValidationContext<T> context, string propertyName, List<ValidationFailure> validationFailures)
        {
            validationFailures.ForEach(error => context.AddFailure(error));
        }
    }
}
