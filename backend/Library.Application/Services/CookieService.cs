using Library.Application.DTOs.TokenDtos;
using Library.Application.Interfaces.Services;
using Library.Application.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Library.Application.Services;

public class CookieService(IOptions<AuthorizationSettings> options) : ICookieService
{
    public void SetTokens(TokenResponse tokens, HttpResponse response)
    {
        response.Cookies.Append("accessToken", tokens.AccessToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.Add(options.Value.Expire)
        });

        response.Cookies.Append("refreshToken", tokens.RefreshToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.Add(options.Value.RefreshExpire)
        });
    }
}
