using FluentValidation;
using Ramada.Service.DTOs.Facilities;

namespace Ramada.Service.Validators.Facilities;

public class FacilityUpdateModelValidator : AbstractValidator<FacilityUpdateModel>
{
    public FacilityUpdateModelValidator()
    {

        RuleFor(facility => facility.Name)
            .NotNull()
            .WithMessage(facility => $"{nameof(facility.Name)} is not specified");

        RuleFor(facility => facility.Description)
        .NotNull()
            .WithMessage(facility => $"{nameof(facility.Description)} is not specified");
    }
}


