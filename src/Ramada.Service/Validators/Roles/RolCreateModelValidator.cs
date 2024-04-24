using FluentValidation;
using Ramada.Service.DTOs.Roles;

namespace Ramada.Service.Validators.Roles;

public class RolCreateModelValidator : AbstractValidator<RoleCreateModel>
{
    public RolCreateModelValidator()
    {
        RuleFor(role => role.Name)
            .NotNull()
            .WithMessage(role => $"{nameof(role.Name)} is not specified");
    }
}
