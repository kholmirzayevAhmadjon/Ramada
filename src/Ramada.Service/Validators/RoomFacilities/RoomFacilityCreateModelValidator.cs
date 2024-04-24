using FluentValidation;
using Ramada.Domain.Entities.Rooms;
using Ramada.Service.DTOs.RoomFacilities;

namespace Ramada.Service.Validators.RoomFacilities;

public class RoomFacilityCreateModelValidator : AbstractValidator<RoomFacilityCreateModel>
{
    public RoomFacilityCreateModelValidator()
    {
        RuleFor(roomFacility => roomFacility.FacilityId)
             .NotNull()
             .NotEqual(0)
             .WithMessage(roomFacility => $"{nameof(roomFacility.FacilityId)} is not specified");

        RuleFor(roomFacility => roomFacility.RoomId)
            .NotNull()
            .NotEqual(0)
            .WithMessage(roomFacility => $"{nameof(roomFacility.RoomId)} is not specified");

        RuleFor(roomFacility => roomFacility.Count)
            .NotNull()
            .NotEqual(0)
            .WithMessage(roomFacility => $"{nameof(roomFacility.Count)} is not specified");
    }
}
