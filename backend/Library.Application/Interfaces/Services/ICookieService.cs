using Library.Application.DTOs.TokenDtos;
using Microsoft.AspNetCore.Http;

namespace Library.Application.Interfaces.Services;

public interface ICookieService
{
    void SetTokens(TokenResponse tokens, HttpResponse response);
}
