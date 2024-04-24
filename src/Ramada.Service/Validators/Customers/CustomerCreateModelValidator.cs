using FluentValidation;
using Ramada.Service.DTOs.Customers;
using Ramada.Service.Validators.Facilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ramada.Service.Validators.Customers;

public class CustomerCreateModelValidator : AbstractValidator<CustomerCreateModel>
{
    public CustomerCreateModelValidator()
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
