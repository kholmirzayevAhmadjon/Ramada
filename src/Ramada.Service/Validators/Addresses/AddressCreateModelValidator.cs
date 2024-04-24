using FluentValidation;
using Ramada.Domain.Entities.Hostels;
using Ramada.Service.DTOs.Addresses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Ramada.Service.Validators.Addresses;

public class AddressCreateModelValidator : AbstractValidator<AddressCreateModel>
{
    public AddressCreateModelValidator()
    {
        RuleFor(address => address.Latitude) 
            .NotNull()
            .WithMessage(address => $"{nameof(address.Latitude)} is not specified");

        RuleFor(address => address.Longitude)
            .NotNull()
            .WithMessage(address => $"{nameof(address.Longitude)} is not specified");
        

        RuleFor(address => address.City)
            .NotNull()
            .WithMessage(address => $"{nameof(address.City)} is not specified");
        

        RuleFor(address => address.Street)
            .NotNull()
            .WithMessage(address => $"{nameof(address.Street)} is not specified");

        RuleFor(address => address.PostCode)
            .NotNull()
            .NotEqual(0)
            .WithMessage(address => $"{nameof(address.PostCode)} is not specified");

        RuleFor(address => address.HouseNumber)
            .NotNull()
            .NotEqual(0)
            .WithMessage(address => $"{nameof(address.HouseNumber)} is not specified");
    }
}
