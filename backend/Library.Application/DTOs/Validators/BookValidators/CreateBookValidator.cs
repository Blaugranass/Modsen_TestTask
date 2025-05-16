using System;
using FluentValidation;
using Library.Application.DTOs.BookDtos;

namespace Library.Application.DTOs.Validators.BookValidators;

public class CreateBookValidator : AbstractValidator<CreateBookDto>
{
    public CreateBookValidator()
    {
        RuleFor(b => b.Name)
            .NotEmpty()
            .MaximumLength(30);

        
        RuleFor(b => b.ISBN)
            .NotEmpty()
            .MaximumLength(30)
            .Matches(@"^\d+$")
            .WithMessage("ISBN must contain only numbers");


        RuleFor(b => b.Description)
            .NotEmpty()
            .MaximumLength(200);
        
    }
}
