using Library.Application.Filters;
using Library.Application.Interfaces.Repositories;
using Library.Application.Pagination;
using Library.Domain.Entities;
using Library.Persistence.Data;
using Library.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Library.Persistence.Repositories;

public class BookRepository(LibraryDbContext dbContext) : BaseRepository<Book>(dbContext), IBookRepository
{
    private readonly LibraryDbContext _dbContext = dbContext;

    public async Task<Book> GetByISBNAsync(string isbn,
        CancellationToken cancellationToken = default)
    {
        return await FirstAsync(b => b.ISBN == isbn, cancellationToken);
    }

    public async Task UpdateAsync(Book book,
        CancellationToken cancellationToken = default)
    {
        await _dbContext.Books
            .Where(b => b.Id == book.Id)
            .ExecuteUpdateAsync(pr => pr
            .SetProperty(b => b.ISBN, book.ISBN)
            .SetProperty(b => b.Author, book.Author)
            .SetProperty(b => b.Genre, book.Genre)
            .SetProperty(b => b.ImageURL, book.ImageURL)
            .SetProperty(b => b.Name, book.Name),
            cancellationToken);
    }

    public async Task<PagedResult<Book>> GetAsync(PageParams pageParams,
        BookFilter filter,
        CancellationToken cancellationToken = default)
    {
        var predicate = filter.ToExpression();

        return await GetPagedAsync(predicate,
            pageParams,
            cancellationToken);
    }
}