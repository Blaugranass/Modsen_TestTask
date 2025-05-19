using System;
using FluentValidation;
using Library.Application.DTOs.AdminDtos;

namespace Library.Application.DTOs.Validators.AdminValidators;

public class LoginAdminValidator : AbstractValidator<LoginAdminDto>
{
    public LoginAdminValidator()
    {
        RuleFor(admin => admin.Mail)
                .NotEmpty() 
                .WithMessage("Email is required")
                .Matches(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")
                .WithMessage("Invalid email format");

        RuleFor(admin => admin.Password)
                .NotEmpty() 
                .WithMessage("Password is required")
                .MinimumLength(6) 
                .WithMessage("Password must be at least 6 characters long")
                .MaximumLength(30)
                .WithMessage("Password must be lower 30 characters long");
    }
}
