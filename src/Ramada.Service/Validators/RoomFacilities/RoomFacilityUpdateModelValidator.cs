using FluentValidation;
using Ramada.Service.DTOs.RoomFacilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ramada.Service.Validators.RoomFacilities;

public class RoomFacilityUpdateModelValidator : AbstractValidator<RoomFacilityUpdateModel>
{
    public RoomFacilityUpdateModelValidator()
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
