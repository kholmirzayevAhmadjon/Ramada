﻿using FluentValidation;
using Ramada.Service.DTOs.Users;
using System.Text.RegularExpressions;

namespace Ramada.Service.Validators.Users;

public class UserCreateModelValidator : AbstractValidator<UserCreateModel>
{
    public UserCreateModelValidator()
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

        RuleFor(user => user.Password)
            .Must(IsPasswordHard);
    }

    private bool IsPasswordHard(string password)
    {
        // Check if the password is at least 8 characters long
        if (password.Length < 8)
            return false;

        // Check if the password contains at least one uppercase letter
        if (!password.Any(char.IsUpper))
            return false;

        // Check if the password contains at least one lowercase letter
        if (!password.Any(char.IsLower))
            return false;

        // Check if the password contains at least one digit
        if (!password.Any(char.IsDigit))
            return false;

        // Check if the password contains at least one special character
        if (!password.Any(ch => !char.IsLetterOrDigit(ch)))
            return false;

        // If all criteria are met, consider the password hard
        return true;
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

