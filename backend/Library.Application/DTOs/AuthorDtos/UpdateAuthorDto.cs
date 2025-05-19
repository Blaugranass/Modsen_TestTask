namespace Library.Application.DTOs.AuthorDtos;

public record class UpdateAuthorDto
(
    string FirstName,
    string  LastName,
    DateOnly Birthday,
    string Country,

    Guid Id
);