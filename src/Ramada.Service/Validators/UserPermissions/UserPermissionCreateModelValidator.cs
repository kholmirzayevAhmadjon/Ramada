using FluentValidation;
using Ramada.Service.DTOs.UserPermissions;

namespace Ramada.Service.Validators.UserPermissions;

public class UserPermissionCreateModelValidator : AbstractValidator<UserPermissionCreateModel>
{
    public UserPermissionCreateModelValidator()
    {
        RuleFor(userPermission => userPermission.UserId)
             .NotNull()
             .NotEqual(0)
             .WithMessage(userPermission => $"{nameof(userPermission.UserId)} is not specified");

        RuleFor(userPermission => userPermission.PermissionId)
            .NotNull()
            .NotEqual(0)
            .WithMessage(userPermission => $"{nameof(userPermission.PermissionId)} is not specified");
    }
}
