using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Library.Application.DTOs.BookDtos;

public record class TakeBookDto
(
    DateTime Taken,
    DateTime DueDate,
    Guid BookId,
    Guid UserId
);
