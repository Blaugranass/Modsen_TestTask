using Library.Application.DTOs.TokenDtos;
using Library.Application.DTOs.UserDtos;
using Library.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController(
        IUserService userService, 
        ICookieService cookieService, 
        IJwtService jwtService) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterUserDto dto)
        {
            await userService.RegisterUserAsync(dto);
            return NoContent();
        } 

        [HttpPost("login")]
        public async Task<ActionResult<TokenResponse>> Login([FromBody] LoginUserDto dto)
        {
            var token = await userService.LoginUserAsync(dto);
            
            cookieService.SetTokens(token, Response);
            return Ok(token);
        }

        [HttpPost("refresh")]
        public async Task<ActionResult<TokenResponse>> RefreshTokenAsync([FromBody] RefreshTokenDto tokenDto)
        {
            var tokens = await jwtService.RefreshTokenAsync(tokenDto.Token);

            cookieService.SetTokens(tokens, Response);
            return Ok(tokens);
        }
    }
}