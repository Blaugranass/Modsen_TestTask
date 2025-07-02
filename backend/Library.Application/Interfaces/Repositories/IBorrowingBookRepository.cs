using Library.Application.Pagination;
using Library.Domain.Entities;

namespace Library.Application.Interfaces.Repositories;

public interface IBorrowingBookRepository : IRepository<BorrowingBook>
{
    Task<bool> IsTakenAsync(Guid bookId, CancellationToken cancellationToken = default);

    Task<PagedResult<Book>> GetAllToUserAsync(Guid userId, PageParams pageParams, CancellationToken cancellationToken = default);

    Task<PagedResult<BorrowingBook>> GetBorrowingBooksAsync(PageParams pageParams, CancellationToken cancellationToken = default);
}