using FluentValidation;
using Ramada.Service.DTOs.Customers;

namespace Ramada.Service.Validators.Customers;

public class CustomerUpdateModelValidator : AbstractValidator<CustomerUpdateModel>
{
    public CustomerUpdateModelValidator() 
    {
        RuleFor(customer => customer.FirstName)
            .NotNull()
            .WithMessage(customer => $"{nameof(customer.FirstName)} is not specified");

        RuleFor(customer => customer.LastName)
            .NotNull()
            .WithMessage(customer => $"{nameof(customer.LastName)} is not specified");

        RuleFor(customer => customer.UserId)
            .NotNull()
            .NotEqual(0)
            .WithMessage(customer => $"{nameof(customer.UserId)} is not specified");
    }
}
