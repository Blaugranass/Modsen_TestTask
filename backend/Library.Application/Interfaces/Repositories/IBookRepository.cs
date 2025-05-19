using Library.Application.Filters;
using Library.Application.Pagination;
using Library.Domain.Entities;

namespace Library.Application.Interfaces.Repositories;

public interface IBookRepository
{
    Task<Book> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<Book> GetByISBNAsync(string isbn, CancellationToken cancellationToken = default);

    Task CreateAsync(Book book, CancellationToken cancellationToken = default);

    Task UpdateAsync(Book book, CancellationToken cancellationToken = default);

    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IEnumerable<Book>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<PagedResult<Book>> GetAsync(PageParams pageParams, BookFilter filter,CancellationToken cancellationToken = default);
}
