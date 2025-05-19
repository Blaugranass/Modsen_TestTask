using Library.Application.DTOs.AuthorDtos;
using Library.Application.DTOs.GenreDtos;

namespace Library.Application.DTOs.BookDtos;

public record class BookResponseDto
(
    Guid Id,
    string Name,
    string Description,
    string ISBN,
    AuthorDto Author,
    GenreDto Genre,
    string? ImageURL
);