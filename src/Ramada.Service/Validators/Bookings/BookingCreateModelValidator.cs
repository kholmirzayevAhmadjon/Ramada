using FluentValidation;
using Ramada.Service.DTOs.Bookings;

namespace Ramada.Service.Validators.Bookings;

public class BookingCreateModelValidator : AbstractValidator<BookingCreateModel>
{
    public BookingCreateModelValidator()
    {
        RuleFor(booking => booking.CustomerId)
            .NotNull()
            .NotEqual(0)
            .WithMessage(booking => $"{nameof(booking.CustomerId)} is not specified");
       
        RuleFor(booking => booking.RoomId)
            .NotNull()
            .NotEqual(0)
            .WithMessage(booking => $"{nameof(booking.RoomId)} is not specified");
       
        RuleFor(booking => booking.NumberOfPeople)
            .NotNull()
            .NotEqual(0)
            .WithMessage(booking => $"{nameof(booking.NumberOfPeople)} is not specified");
       
        RuleFor(booking => booking.NumberOfDays)
            .NotNull()
            .NotEqual(0)
            .WithMessage(booking => $"{nameof(booking.NumberOfDays)} is not specified");
       
        RuleFor(booking => booking.StartDate)
            .NotNull()
            .WithMessage(booking => $"{nameof(booking.StartDate)} is not specified");
    }
}
