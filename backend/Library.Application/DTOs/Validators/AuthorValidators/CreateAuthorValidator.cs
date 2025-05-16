using FluentValidation;
using Library.Application.DTOs.AuthorDtos;

namespace Library.Application.DTOs.Validators.AuthorValidators;

public class CreateAuthorValidator : AbstractValidator<CreateAuthorDto>
{
    public CreateAuthorValidator()
    {
        RuleFor(a => a.FirstName)
            .NotEmpty()
            .MaximumLength(20);

        RuleFor(a => a.LastName)
            .NotEmpty()
            .MaximumLength(20);

        RuleFor(a => a.Country)
            .NotEmpty()
            .MaximumLength(20);

        RuleFor(a => a.Birthday)
            .NotEmpty()
            .Must(b => b <= DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-18)));
    }
}
