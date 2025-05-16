using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Library.Application.DTOs.TokenDtos;
using Library.Application.Interfaces.Repositories;
using Library.Application.Interfaces.Services;
using Library.Application.Settings;
using Library.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Library.Application.Services;

public class JwtService(
    IOptions<AuthorizationSettings> options, 
    IRefreshTokenRepository refreshTokenRepository,
    IAuthUserRepository authUserRepository): IJwtService
{

    public string GenerateAccessToken(AuthUser authUser)
    {
        var role = authUser is Admin ? "Admin" : "User";
        var claims = new List<Claim>
        {
            new (ClaimTypes.Role, role),
            new ("userId", authUser.Id.ToString())
        };

        var jwtToken = new JwtSecurityToken(
            expires: DateTime.UtcNow.Add(options.Value.Expire),
            claims: claims,
            signingCredentials: 
            new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(options.Value.SecretKey)),
                        SecurityAlgorithms.HmacSha256));

        return new JwtSecurityTokenHandler().WriteToken(jwtToken);
    }

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    public async Task<TokenResponse> RefreshTokenAsync(string refreshToken)
    {
        var storedToken = await refreshTokenRepository.GetByTokenAsync(refreshToken)
            ?? throw new SecurityTokenException("Invalid token");

        if (DateTime.UtcNow > storedToken.Expires || storedToken.IsRevoked)
            throw new SecurityTokenException("Token expired or revoked");

        var authUser = await authUserRepository.GetByIdAsync(storedToken.UserId);

        storedToken.IsRevoked = true;
        await refreshTokenRepository.UpdateAsync(storedToken);

        var newAccessToken = GenerateAccessToken(authUser);
        var newRefreshToken = GenerateRefreshToken();

        await refreshTokenRepository.CreateAsync(new RefreshToken
        {
            Token = newRefreshToken,
            Expires = DateTime.UtcNow.Add(options.Value.Expire),
            UserId = authUser.Id,
            IsRevoked = false
        });

        return new TokenResponse(newAccessToken, newRefreshToken);
    }
}
