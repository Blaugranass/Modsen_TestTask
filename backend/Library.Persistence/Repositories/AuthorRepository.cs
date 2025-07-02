using System.Linq.Expressions;
using Library.Application.Filters;
using Library.Application.Interfaces.Repositories;
using Library.Application.Pagination;
using Library.Domain.Entities;
using Library.Persistence.Data;
using Library.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Library.Persistence.Repositories;

public class AuthorRepository(LibraryDbContext dbContext) :  BaseRepository<Author>(dbContext), IAuthorRepository
{
    private readonly LibraryDbContext _dbContext = dbContext;

    public async Task UpdateAsync(Author author,
        CancellationToken cancellationToken = default)
    {
        await _dbContext.Authors
            .Where(a => a.Id == author.Id)
            .ExecuteUpdateAsync(pr => pr
            .SetProperty(a => a.LastName, author.LastName)
            .SetProperty(a => a.FirstName, author.FirstName)
            .SetProperty(a => a.Birthday, author.Birthday)
            .SetProperty(a => a.Country, author.Country), cancellationToken);
    }

    public async Task<PagedResult<Book>> GetAllBooksToAuthorAsync(Guid authorId,
        PageParams pageParams,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Authors
            .AsNoTracking()
            .Where(a => a.Id == authorId)
            .SelectMany(a => a.Books)
            .ToPagedAsync(pageParams, cancellationToken);
    }

    public async Task<PagedResult<Author>> GetAuthorsAsync(AuthorFilter filter,
        PageParams pageParams,
        CancellationToken cancellationToken = default)
    {
        var predicate = filter.ToExpression();

        return await GetPagedAsync(predicate,
            pageParams,
            cancellationToken);
    }
}