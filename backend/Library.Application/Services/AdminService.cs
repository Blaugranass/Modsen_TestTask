using Library.Application.DTOs.AdminDtos;
using Library.Application.DTOs.TokenDtos;
using Library.Application.Exceptions;
using Library.Application.Interfaces.Repositories;
using Library.Application.Interfaces.Services;
using Library.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Library.Application.Services;

public class AdminService(
    IAuthUserRepository authUserRepository,
    IJwtService jwtService, 
    IRefreshTokenRepository refreshTokenRepository) : IAdminService
{
    public async Task<TokenResponse> LoginAdminAsync(LoginAdminDto loginAdminDto)
    {
        var authUser = await authUserRepository.GetByMailAsync(loginAdminDto.Mail) 
            ?? throw new UnauthorizedException("Invalid credentials");

        if(authUser is not Admin admin)
            throw new ForbiddenException($"User with mail '{loginAdminDto.Mail}' is not an admin");

        var result = new PasswordHasher<Admin>()
            .VerifyHashedPassword(admin, admin.PassHash, loginAdminDto.Password);
        
        if (result == PasswordVerificationResult.Success)
        {
            var accessToken = jwtService.GenerateAccessToken(admin);
            var refreshToken = jwtService.GenerateRefreshToken();
            await refreshTokenRepository.CreateAsync(new RefreshToken
            {
                Token = refreshToken,
                Expires = DateTime.UtcNow.AddDays(5),
                UserId = admin.Id,
                IsRevoked = false
            });

            return new TokenResponse(accessToken,refreshToken);
        }
        else
        {
            throw new UnauthorizedException("Invalid credentials");
        }
    }
}
