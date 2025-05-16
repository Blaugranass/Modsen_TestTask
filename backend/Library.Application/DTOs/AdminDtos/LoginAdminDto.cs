using System.ComponentModel.DataAnnotations;

namespace Library.Application.DTOs.AdminDtos;

public record class LoginAdminDto
(
    string Mail,
    string Password
);