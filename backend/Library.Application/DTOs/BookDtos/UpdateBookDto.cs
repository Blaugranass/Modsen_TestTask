using System.ComponentModel.DataAnnotations;
using Library.Domain.Entities;

namespace Library.Application.DTOs.BookDtos;

public record class UpdateBookDto
(
    string Name,
    string Description,
    Guid AuthorId,
    Guid BookGenreId,
    string ISBN,
    Guid Id
);
