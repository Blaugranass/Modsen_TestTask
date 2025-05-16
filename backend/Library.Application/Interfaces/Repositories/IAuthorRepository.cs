using Library.Application.Filters;
using Library.Application.Pagination;
using Library.Domain.Entities;

namespace Library.Application.Interfaces.Repositories;

public interface IAuthorRepository
{
    Task<Author> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task CreateAsync(Author author, CancellationToken cancellationToken = default);

    Task UpdateAsync(Author author, CancellationToken cancellationToken = default);

    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    Task<PagedResult<Book>> GetAllBooksToAuthorAsync(Guid authorId, PageParams pageParams, CancellationToken cancellationToken = default);

    Task<PagedResult<Author>> GetAuthorsAsync(AuthorFilter filter, PageParams pageParams, CancellationToken cancellationToken = default);

}