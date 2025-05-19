using AutoMapper;
using Library.Application.DTOs.TokenDtos;
using Library.Application.DTOs.UserDtos;
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
            ?? throw new Exception($"User with mail {loginUserDto.Mail} not found");

        if(authUser is not User user)
            throw new Exception($"User {loginUserDto.Mail} is not an admin");

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
            throw new Exception("Wrong password");
        }

    }

    public async Task RegisterUserAsync(RegisterUserDto registerUserDto)
    {
        var user = mapper.Map<User>(registerUserDto);

        if(await authUserRepository.ExistWithMail(registerUserDto.Mail))
        {
            throw new Exception("User with this mail already create");
        }
        
        user.PassHash = new PasswordHasher<User>().HashPassword(user, registerUserDto.Password);

        await authUserRepository.CreateAsync(user);
    }
}