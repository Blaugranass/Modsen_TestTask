using Library.Application.DTOs.AuthorDtos;
using Library.Application.DTOs.GenreDtos;

namespace Library.Application.DTOs.BookDtos;

public record class BookDto
(
    string Name,
    GenreDto Genre,
    AuthorDto Author
);