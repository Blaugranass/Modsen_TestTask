using AutoMapper;
using Library.Application.DTOs.TokenDtos;
using Library.Application.DTOs.UserDtos;
using Library.Application.Exceptions;
using Library.Application.Interfaces.Repositories;
using Library.Application.Interfaces.Services;
using Library.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Library.Application.Services;

public class UserService(
    IAuthUserRepository authUserRepository, 
    IJwtService jwtService, 
    IMapper mapper,
    IRefreshTokenRepository refreshTokenRepository) : IUserService
{
    public async Task<TokenResponse> LoginUserAsync(LoginUserDto loginUserDto)
    {
        var authUser = await authUserRepository.GetByMailAsync(loginUserDto.Mail) 
            ?? throw new UnauthorizedException("Invalid credentials");

        if(authUser is not User user)
            throw new ForbiddenException($"User {loginUserDto.Mail} is not an admin");

        var result = new PasswordHasher<User>().VerifyHashedPassword(user, user.PassHash, loginUserDto.Password);
        
        if (result == PasswordVerificationResult.Success)
        {
            var accessToken = jwtService.GenerateAccessToken(user);
            var refreshToken = jwtService.GenerateRefreshToken();
            await refreshTokenRepository.CreateAsync(new RefreshToken
            {
                Token = refreshToken,
                Expires = DateTime.UtcNow.AddDays(5),
                UserId = user.Id,
                IsRevoked = false
            });

            return new TokenResponse(accessToken,refreshToken);
        }
        else
        {
            throw new UnauthorizedException("Invalid credentials");
        }

    }

    public async Task RegisterUserAsync(RegisterUserDto registerUserDto)
    {
        var user = mapper.Map<User>(registerUserDto);

        if(await authUserRepository.ExistWithMail(registerUserDto.Mail))
        {
            throw new ConflictException($"User with this mail '{registerUserDto.Mail}' already create");
        }
        
        user.PassHash = new PasswordHasher<User>().HashPassword(user, registerUserDto.Password);

        await authUserRepository.CreateAsync(user);
    }
}