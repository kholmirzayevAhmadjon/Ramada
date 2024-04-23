using FluentValidation;
using FluentValidation.Results;
using Ramada.Service.Exceptions;

namespace Ramada.Service.Extensions;

public static class ValidationExtension
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
