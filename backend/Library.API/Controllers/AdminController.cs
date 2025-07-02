using Library.Application.DTOs.AdminDtos;
using Library.Application.DTOs.TokenDtos;
using Library.Application.Interfaces.Services;
using Library.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [Route("api/admins")]
    [ApiController]
    public class AdminController(IAdminService adminService, IJwtService jwtService, ICookieService cookieService) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<ActionResult<TokenResponse>> LoginAdminAsync([FromBody] LoginAdminDto loginAdminDto)
        {
            var tokens = await adminService.LoginAdminAsync(loginAdminDto);
  
            cookieService.SetTokens(tokens, Response);
            return Ok(tokens);
        }

        [HttpPost("refresh")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<ActionResult<TokenResponse>> RefreshTokenAsync([FromBody] RefreshTokenDto tokenDto)
        {
            var tokens = await jwtService.RefreshTokenAsync(tokenDto.Token);

            cookieService.SetTokens(tokens, Response);
            return Ok(tokens);
        }

    }
}
