using System;
using Library.Application.Interfaces.Repositories;
using Library.Persistence.Data;
using Library.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Persistence.DI;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddDbContext<LibraryDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")))
            .AddTransient<IAuthorRepository, AuthorRepository>()
            .AddTransient<IAuthUserRepository, AuthUserRepository>()
            .AddTransient<IBookRepository, BookRepository>()
            .AddTransient<IBorrowingBookRepository, BorrowingBookRepository>()
            .AddTransient<IGenreRepository,GenreRepository>()
            .AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();

        return services;
            
    }
}
