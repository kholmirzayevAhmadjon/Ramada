using FluentValidation;
using Ramada.Service.DTOs.Roles;

namespace Ramada.Service.Validators.Roles;

public class RoleUpdateModelValidator : AbstractValidator<RoleUpdateeModel>
{
    public RoleUpdateModelValidator()
    {
        RuleFor(role => role.Name)
            .NotNull()
            .WithMessage(role => $"{nameof(role.Name)} is not specified");
    }
}
