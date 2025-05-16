using System;
using System.Text;
using Library.Application.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Library.Application.Extensions;

public static class AuthExtensions
{
    public static IServiceCollection AddAuth(this IServiceCollection serviceCollections, IConfiguration configuration)
    {
        var authSettings = configuration.GetSection(nameof(AuthorizationSettings)).Get<AuthorizationSettings>();
        serviceCollections.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer( o =>
        {
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authSettings!.SecretKey))
            };

        });

        return serviceCollections;
    }
}   
