using FluentValidation;
using Ramada.Domain.Entities.Users;

namespace Ramada.Service.Validators.Permissions;

public class PermissionCreateModelValidator : AbstractValidator<PermissionCreateModel>
{
    public PermissionCreateModelValidator()
    {
        RuleFor(permission => permission.Method)
           .NotNull()
           .WithMessage(permission => $"{nameof(permission.Method)} is not specified");
        
        RuleFor(permission => permission.Controller)
           .NotNull()
           .WithMessage(permission => $"{nameof(permission.Controller)} is not specified");
    }
}
