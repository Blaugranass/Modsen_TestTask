using Library.Domain.Entities;
using Library.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Library.Persistence.Data;

public class LibraryDbContext(DbContextOptions<LibraryDbContext> options) : DbContext(options)
{

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(AuthorConfiguration).Assembly);
        builder.ApplyConfigurationsFromAssembly(typeof(BookConfiguration).Assembly);
        builder.ApplyConfigurationsFromAssembly(typeof(BorrowingBookConfiguration).Assembly);
        builder.ApplyConfigurationsFromAssembly(typeof(GenreConfiguration).Assembly);
        builder.ApplyConfigurationsFromAssembly(typeof(RefreshToken).Assembly);
        builder.ApplyConfigurationsFromAssembly(typeof(AuthUserConfiguration).Assembly);
    }

    public DbSet<Book> Books { get; set; }

    public DbSet<Genre> Genres { get; set; }

    public DbSet<Author> Authors { get; set; }

    public DbSet<BorrowingBook> BorrowingBooks { get; set; }

    public DbSet<RefreshToken> RefreshTokens { get; set; }

    public DbSet<AuthUser> AuthUsers { get; set; }
    
}