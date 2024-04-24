using FluentValidation;
using Ramada.Service.DTOs.Addresses;

namespace Ramada.Service.Validators.Addresses;

public class AddressUpdateModelValidator : AbstractValidator<AddressUpdateModel>
{
    public AddressUpdateModelValidator()
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
