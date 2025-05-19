namespace Library.Application.DTOs.AuthorDtos;

public record class AuthorDto
(
    Guid Id,
    string LastName,
    string FirstName,
    DateOnly Birthday,
    string Country
);