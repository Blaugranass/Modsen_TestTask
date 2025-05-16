using Library.Application.DTOs.TokenDtos;
using Library.Application.DTOs.UserDtos;

namespace Library.Application.Interfaces.Services;

public interface IUserService
{
    Task<TokenResponse> LoginUserAsync(LoginUserDto dto);

    Task RegisterUserAsync(RegisterUserDto dto);
}
