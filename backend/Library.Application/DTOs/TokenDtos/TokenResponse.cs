namespace Library.Application.DTOs.TokenDtos;

public record class TokenResponse
(
    string AccessToken, 
    string RefreshToken
);
