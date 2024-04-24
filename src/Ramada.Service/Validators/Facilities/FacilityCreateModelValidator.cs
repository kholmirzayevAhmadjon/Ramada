using FluentValidation;
using Ramada.Domain.Entities.Rooms;
using Ramada.Service.DTOs.Facilities;

namespace Ramada.Service.Validators.Facilities;

public class FacilityCreateModelValidator : AbstractValidator<FacilityCreateModel>
{
    public FacilityCreateModelValidator()
    {

        RuleFor(facility => facility.Name)
            .NotNull()
            .WithMessage(facility => $"{nameof(facility.Name)} is not specified");

        RuleFor(facility => facility.Description)
        .NotNull()
            .WithMessage(facility => $"{nameof(facility.Description)} is not specified");
    }
}


