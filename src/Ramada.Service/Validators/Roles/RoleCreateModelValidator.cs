using FluentValidation;
using Ramada.Service.DTOs.Roles;

namespace Ramada.Service.Validators.Roles;

public class RoleCreateModelValidator : AbstractValidator<RoleCreateModel>
{
    public RoleCreateModelValidator()
    {
        RuleFor(role => role.Name)
            .NotNull()
            .WithMessage(role => $"{nameof(role.Name)} is not specified");
    }
}
