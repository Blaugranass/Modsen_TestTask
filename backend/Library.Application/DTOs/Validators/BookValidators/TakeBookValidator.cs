using System;
using System.Data;
using FluentValidation;
using Library.Application.DTOs.BookDtos;

namespace Library.Application.DTOs.Validators.BookValidators;

public class TakeBookValidator : AbstractValidator<TakeBookDto>
{
    public TakeBookValidator()
    {
        RuleFor(b => b.Taken)
            .LessThan(b => b.DueDate)
            .WithMessage("Taken time must be lower than due date");

        RuleFor(b => b.DueDate)
            .GreaterThan(DateTime.Today)
            .WithMessage("Due date must be higher than today");

        RuleFor(b => b.Taken)
            .GreaterThanOrEqualTo(DateTime.Today)
            .WithMessage("Taken time must be today or higher");
    }
}
