using Library.Application.DTOs.BookDtos;
using Library.Application.DTOs.BorrowingBookDtos;
using Library.Application.Pagination;
using Library.Domain.Entities;

namespace Library.Application.Interfaces.Services;

public interface IBorrowingService
{
    Task<PagedResult<BookResponseDto>> GetAllBooksToUserAsync(Guid UserId, PageParams pageParams, CancellationToken cancellationToken = default);

    Task<PagedResult<BorrowingBookResponseDto>> GetAllBorrowingBooksAsync(PageParams pageParams, CancellationToken cancellationToken = default);

    Task DeleteBorrowingBookAsync(Guid id, CancellationToken cancellationToken= default);

}