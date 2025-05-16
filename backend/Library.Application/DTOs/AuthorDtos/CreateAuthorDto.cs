namespace Library.Application.DTOs.AuthorDtos;

public record class CreateAuthorDto
(
    string FirstName,
    string LastName,
    DateOnly Birthday,
    string Country
);