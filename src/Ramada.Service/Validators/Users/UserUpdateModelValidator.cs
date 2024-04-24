using FluentValidation;
using Ramada.Service.DTOs.Users;
using System.Text.RegularExpressions;

namespace Ramada.Service.Validators.Users
{
    public class UserUpdateModelValidator : AbstractValidator<UserUpdateModel>
    {
        public UserUpdateModelValidator()
        {
            RuleFor(user => user.RoleId)
                .NotNull()
                .NotEqual(0)
                .WithMessage(user => $"{nameof(user.RoleId)} is not specified");

            RuleFor(user => user.Phone)
                .NotNull()
                .WithMessage(user => $"{nameof(user.Phone)} is not specified");

            RuleFor(user => user.Phone)
                .Must(IsPhoneValid);

            RuleFor(user => user.Email)
                .NotNull()
                .WithMessage(user => $"{nameof(user.Email)} is not specified");

            RuleFor(user => user.Email)
                .Must(IsEmailValid);
        }

        private bool IsPhoneValid(string phone)
        {
            string pattern = @"^\+998\d{9}$";

            return Regex.IsMatch(phone, pattern);
        }

        private bool IsEmailValid(string email)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            return Regex.IsMatch(email, pattern);
        }

    }
}
