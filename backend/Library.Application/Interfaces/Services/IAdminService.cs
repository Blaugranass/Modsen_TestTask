using Library.Application.DTOs.AdminDtos;
using Library.Application.DTOs.TokenDtos;

namespace Library.Application.Interfaces.Services;

public interface IAdminService
{
    Task<TokenResponse> LoginAdminAsync(LoginAdminDto loginAdminDto);
}
