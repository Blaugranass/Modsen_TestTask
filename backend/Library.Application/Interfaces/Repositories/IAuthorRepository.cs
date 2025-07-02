using Library.Application.Filters;
using Library.Application.Pagination;
using Library.Domain.Entities;

namespace Library.Application.Interfaces.Repositories;

public interface IAuthorRepository : IRepository<Author>
{
    Task UpdateAsync(Author author, CancellationToken cancellationToken = default);

    Task<PagedResult<Book>> GetAllBooksToAuthorAsync(Guid authorId, PageParams pageParams, CancellationToken cancellationToken = default);

    Task<PagedResult<Author>> GetAuthorsAsync(AuthorFilter filter, PageParams pageParams, CancellationToken cancellationToken = default);

}