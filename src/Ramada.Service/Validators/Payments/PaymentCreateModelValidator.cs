using FluentValidation;
using Ramada.Service.DTOs.Payments;

namespace Ramada.Service.Validators.Payments;

public class PaymentCreateModelValidator : AbstractValidator<PaymentCreateModel>
{
    public PaymentCreateModelValidator()
    {
        RuleFor(payment => payment.BookingId)
           .NotNull()
           .NotEqual(0)
           .WithMessage(payment => $"{nameof(payment.BookingId)} is not specified");

        RuleFor(payment => payment.TotalPrice)
            .NotNull()
            .NotEqual(0)
            .WithMessage(payment => $"{nameof(payment.TotalPrice)} is not specified");
    }
}
