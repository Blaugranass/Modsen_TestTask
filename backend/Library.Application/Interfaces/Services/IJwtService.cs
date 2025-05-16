using Library.Application.DTOs.TokenDtos;
using Library.Domain.Entities;

namespace Library.Application.Interfaces.Services;

public interface IJwtService
{
    string GenerateAccessToken(AuthUser authUser);

    public string GenerateRefreshToken();

    Task<TokenResponse> RefreshTokenAsync(string refreshToken);
}
