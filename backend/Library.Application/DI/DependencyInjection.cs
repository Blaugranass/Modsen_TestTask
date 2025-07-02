using System.Security.Claims;
using FluentValidation;
using Library.Application.DTOs.AdminDtos;
using Library.Application.DTOs.AuthorDtos;
using Library.Application.DTOs.BookDtos;
using Library.Application.DTOs.UserDtos;
using Library.Application.DTOs.Validators.AdminValidators;
using Library.Application.DTOs.Validators.AuthorValidators;
using Library.Application.DTOs.Validators.BookValidators;
using Library.Application.DTOs.Validators.UserValidators;
using Library.Application.Extensions;
using Library.Application.Interfaces.Services;
using Library.Application.Mappers;
using Library.Application.Services;
using Library.Application.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

namespace Library.Application.DI;

public static class DependencyInjection
{
    public static IServiceCollection AddAutoMapper(this IServiceCollection services)
    {
        services
            .AddAutoMapper(typeof(BookProfile))
            .AddAutoMapper(typeof(AuthorProfile))
            .AddAutoMapper(typeof(UserProfile))
            .AddAutoMapper(typeof(BorrowingBookProfile));

        return services;
    }

    public static IServiceCollection AddAuthorization(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.
            AddAuth(configuration)
            .Configure<AuthorizationSettings>(configuration.GetSection("AuthorizationSettings"));
        
        return services;
    }

    public static IServiceCollection AddAuthorizationPolicies(this IServiceCollection services)
    {
        services.AddAuthorizationBuilder()
            .AddPolicy(AuthorizationPolicies.Admin, policy =>
                policy.RequireClaim(ClaimTypes.Role, AuthorizationPolicies.AdminRole))
            .AddPolicy(AuthorizationPolicies.User, policy =>
                policy.RequireClaim(ClaimTypes.Role, AuthorizationPolicies.UserRole))
            .AddPolicy(AuthorizationPolicies.AdminOrUser, policy =>
                policy.RequireAssertion(context =>
                    context.User.HasClaim(c =>
                        (c.Type == ClaimTypes.Role && c.Value == AuthorizationPolicies.AdminRole) ||
                        (c.Type == ClaimTypes.Role && c.Value == AuthorizationPolicies.UserRole)
                    )));

        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services
            .AddScoped<IAdminService, AdminService>()
            .AddScoped<IAuthorService, AuthorService>()
            .AddScoped<IUserService, UserService>()
            .AddScoped<IBookService, BookService>()
            .AddScoped<IBorrowingService, BorrowingBookService>()
            .AddScoped<ICookieService, CookieService>()
            .AddScoped<IJwtService, JwtService>();

        return services;
    }

    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services
            .AddScoped<IValidator<LoginUserDto>, LoginUserValidator>()
            .AddScoped<IValidator<RegisterUserDto>, RegisterUserValidator>()
            .AddScoped<IValidator<CreateBookDto>, CreateBookValidator>()
            .AddScoped<IValidator<TakeBookDto>, TakeBookValidator>()
            .AddScoped<IValidator<UpdateBookDto>, UpdateBookValidator>()
            .AddScoped<IValidator<CreateAuthorDto>, CreateAuthorValidator>()
            .AddScoped<IValidator<UpdateAuthorDto>, UpdateAuthorValidator>()
            .AddScoped<IValidator<LoginAdminDto>, LoginAdminValidator>()
            .AddFluentValidationAutoValidation();

        return services;
    }
}