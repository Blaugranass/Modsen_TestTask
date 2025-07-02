using Library.Application.Interfaces.Repositories;
using Library.Persistence.Data;
using Library.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Persistence.DI;

public static class DependencyInjection
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services
            .AddScoped<IAuthorRepository, AuthorRepository>()
            .AddScoped<IAuthUserRepository, AuthUserRepository>()
            .AddScoped<IBookRepository, BookRepository>()
            .AddScoped<IBorrowingBookRepository, BorrowingBookRepository>()
            .AddScoped<IGenreRepository, GenreRepository>()
            .AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

        return services;

    }

    public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddDbContext<LibraryDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        return services;
    }
}
