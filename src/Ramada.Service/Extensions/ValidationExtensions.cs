using FluentValidation;
using FluentValidation.Results;
using Ramada.Service.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ramada.Service.Extensions;

public static class ValidationExtensions
{
        public static async Task<ValidationResult> ValidateOrPanicAsync<TValidator, TObject>(this TValidator validator,
            TObject @object)
            where TObject : class
            where TValidator : AbstractValidator<TObject>
        {
            var validationResult = await validator.ValidateAsync(@object);
            if (validationResult.Errors.Any())
                throw new ArgumentIsNotValidException(validationResult.Errors.First().ErrorMessage);

            return validationResult;
        }
}
