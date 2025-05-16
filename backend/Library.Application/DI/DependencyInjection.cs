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
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services
        .AddAutoMapper(typeof(BookProfile))
        .AddAutoMapper(typeof(AuthorProfile))
        .AddAutoMapper(typeof(UserProfile))
        .AddAutoMapper(typeof(BorrowingBookProfile))
        .AddAuth(configuration)
        .Configure<AuthorizationSettings>(configuration.GetSection("AuthorizationSettings"))
        .AddScoped<IValidator<LoginUserDto>, LoginUserValidator>()
        .AddScoped<IValidator<RegisterUserDto>, RegisterUserValidator>()
        .AddScoped<IValidator<CreateBookDto>, CreateBookValidator>()
        .AddScoped<IValidator<TakeBookDto>, TakeBookValidator>()
        .AddScoped<IValidator<UpdateBookDto>, UpdateBookValidator>()
        .AddScoped<IValidator<CreateAuthorDto>, CreateAuthorValidator>()
        .AddScoped<IValidator<UpdateAuthorDto>, UpdateAuthorValidator>()
        .AddScoped<IValidator<LoginAdminDto>, LoginAdminValidator>()
        .AddFluentValidationAutoValidation()
        .AddScoped<IAdminService, AdminService>()
        .AddScoped<IAuthorService, AuthorService>()
        .AddScoped<IUserService, UserService>()
        .AddScoped<IBookService, BookService>()
        .AddScoped<IBorrowingService, BorrowingBookService>()
        .AddScoped<ICookieService, CookieService>()
        .AddScoped<IJwtService, JwtService>();
        return services;
    }
}