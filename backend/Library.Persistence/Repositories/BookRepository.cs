using Library.Application.Filters;
using Library.Application.Interfaces.Repositories;
using Library.Application.Pagination;
using Library.Domain.Entities;
using Library.Persistence.Data;
using Library.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Library.Persistence.Repositories;

public class BookRepository(LibraryDbContext dbContext) : IBookRepository
{
    public async Task CreateAsync(Book book, CancellationToken cancellationToken = default)
    {
        await dbContext.Books
            .AddAsync(book, cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await dbContext.Books
            .Where(b => b.Id == id)
            .ExecuteDeleteAsync(cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<Book> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await dbContext.Books
            .AsNoTracking()
            .Where(b => b.Id == id)
            .FirstAsync(cancellationToken);
    }

    public async Task<Book> GetByISBNAsync(string isbn, CancellationToken cancellationToken = default)
    {
        return await dbContext.Books
            .AsNoTracking()
            .Where(b => b.ISBN == isbn)
            .FirstAsync(cancellationToken); 
    }

    public async Task UpdateAsync(Book book, CancellationToken cancellationToken = default)
    {
        await dbContext.Books
            .Where(b => b.Id == book.Id)
            .ExecuteUpdateAsync(pr => pr
            .SetProperty(b => b.ISBN, book.ISBN)
            .SetProperty(b => b.Author, book.Author)
            .SetProperty(b => b.Genre, book.Genre)
            .SetProperty(b => b.ImageURL, book.ImageURL)
            .SetProperty(b => b.Name, book.Name),
            cancellationToken);
        
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<Book>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.Books
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<PagedResult<Book>> GetAsync(PageParams pageParams, BookFilter filter, CancellationToken cancellationToken = default)
    {
        return await dbContext.Books
            .AsNoTracking()
            .ToPagedAsync(pageParams, cancellationToken);
    }
}
