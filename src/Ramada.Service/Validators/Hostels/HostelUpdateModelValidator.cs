using FluentValidation;
using Ramada.Service.DTOs.Hostels;

namespace Ramada.Service.Validators.Hostels;

public class HostelUpdateModelValidator : AbstractValidator<HostelUpdateModel>
{
    public HostelUpdateModelValidator()
    {
        RuleFor(hostel => hostel.UserId)
            .NotNull()
            .NotEqual(0)
            .WithMessage(hostel => $"{nameof(hostel.UserId)} is not specified");

        RuleFor(hostel => hostel.AddressId)
            .NotNull()
            .NotEqual(0)
            .WithMessage(hostel => $"{nameof(hostel.AddressId)} is not specified");

        RuleFor(hostel => hostel.Name)
            .NotNull()
            .WithMessage(hostel => $"{nameof(hostel.Name)} is not specified");

        RuleFor(hostel => hostel.Description)
            .NotNull()
            .WithMessage(hostel => $"{nameof(hostel.Description)} is not specified");
    }
}
