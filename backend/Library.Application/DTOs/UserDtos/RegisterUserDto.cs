namespace Library.Application.DTOs.UserDtos;

public record class RegisterUserDto
(
    string Mail,
    string Number,
    string Password
);