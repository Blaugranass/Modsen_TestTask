using Library.Application.DTOs.BookDtos;
using Library.Application.Filters;
using Library.Application.Pagination;
using Library.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Library.Application.Interfaces.Services;

public interface IBookService
{
    Task<Book> GetBookByIdAsync(Guid id, CancellationToken cancellationToken);

    Task CreateBookAsync(CreateBookDto dto, CancellationToken cancellationToken);

    Task UpdateBookAsync(UpdateBookDto dto, CancellationToken cancellationToken);

    Task<Book> GetBookByISBNAsync(string isbn, CancellationToken cancellationToken);

    Task DeleteBookAsync(Guid id, CancellationToken cancellationToken);

    Task AddPictureAsync(Guid id, IFormFile file, CancellationToken cancellationToken);

    Task<PagedResult<BookResponseDto>> GetBooksAsync(PageParams pageParams, BookFilter filter, CancellationToken cancellationToken);

    Task TakeBookAsync(TakeBookDto dto, CancellationToken cancellationToken);

}
