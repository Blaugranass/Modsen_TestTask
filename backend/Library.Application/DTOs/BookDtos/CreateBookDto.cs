using System.ComponentModel.DataAnnotations;
using Library.Domain.Entities;

namespace Library.Application.DTOs.BookDtos;

public record class CreateBookDto
(
    string Name,
    Guid AuthorId,
    Guid GenreId,
    string Description,
    string ISBN
);
