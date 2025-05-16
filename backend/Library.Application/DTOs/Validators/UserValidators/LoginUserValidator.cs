using System;
using FluentValidation;
using Library.Application.DTOs.UserDtos;

namespace Library.Application.DTOs.Validators.UserValidators;

public class LoginUserValidator : AbstractValidator<LoginUserDto>
{
    public LoginUserValidator()
    {
        RuleFor(u => u.Mail)
            .NotEmpty() 
            .WithMessage("Email is required")
            .Matches(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")
            .WithMessage("Invalid email format");

        RuleFor(u => u.Password)
            .NotEmpty() 
            .WithMessage("Password is required")
            .MinimumLength(6) 
            .WithMessage("Password must be at least 6 characters long")
            .MaximumLength(30)
            .WithMessage("Password must be lower 30 characters long");

    }
}
