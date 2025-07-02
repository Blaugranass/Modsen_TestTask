using AutoMapper;
using Library.Application.DTOs.BookDtos;
using Library.Application.DTOs.BorrowingBookDtos;
using Library.Application.Exceptions;
using Library.Application.Interfaces.Repositories;
using Library.Application.Interfaces.Services;
using Library.Application.Pagination;
using Library.Domain.Entities;

namespace Library.Application.Services;

public class BorrowingBookService(IBorrowingBookRepository BorrowingBookRepository, IMapper mapper) : IBorrowingService
{
    public async Task<PagedResult<BookResponseDto>> GetAllBooksToUserAsync(
        Guid UserId,
        PageParams pageParams,
        CancellationToken cancellationToken = default)
    {
        var books = await BorrowingBookRepository.GetAllToUserAsync(UserId, pageParams, cancellationToken);

        return mapper.Map<PagedResult<BookResponseDto>>(books);
    }

    public async Task<PagedResult<BorrowingBookResponseDto>> GetAllBorrowingBooksAsync(
        PageParams pageParams,
        CancellationToken cancellationToken = default)
    {
        var borrowingBooks = await BorrowingBookRepository.GetBorrowingBooksAsync(pageParams, cancellationToken)
            ?? throw new NotFoundException("Taken Books not found");

        return mapper.Map<PagedResult<BorrowingBookResponseDto>>(borrowingBooks);
    }

    public async Task DeleteBorrowingBookAsync(Guid id, CancellationToken cancellationToken= default)
    {
        await BorrowingBookRepository.DeleteAsync(id, cancellationToken);
    }
}
