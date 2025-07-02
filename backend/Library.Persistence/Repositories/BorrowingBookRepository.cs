using System;
using Library.Application.Interfaces.Repositories;
using Library.Application.Pagination;
using Library.Domain.Entities;
using Library.Persistence.Data;
using Library.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Library.Persistence.Repositories;

public class BorrowingBookRepository(LibraryDbContext dbContext) : BaseRepository<BorrowingBook>(dbContext), IBorrowingBookRepository
{

    private readonly LibraryDbContext _dbContext = dbContext;

    public async Task<PagedResult<Book>> GetAllToUserAsync(Guid userId, 
        PageParams pageParams, 
        CancellationToken cancellationToken = default)
    {
        var books = _dbContext.BorrowingBooks
            .Include(b => b.Book)
            .AsNoTracking()
            .Where(b => b.UserId == userId)
            .Select(b => b.Book);

        return await books.ToPagedAsync(pageParams, cancellationToken);
    }

    public async Task<PagedResult<BorrowingBook>> GetBorrowingBooksAsync(
        PageParams pageParams, 
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.BorrowingBooks
            .AsNoTracking()
            .Include(b => b.Book)
            .ToPagedAsync(pageParams, cancellationToken);
    }

    public async Task<bool> IsTakenAsync(Guid bookId,
        CancellationToken cancellationToken = default)
    {
        return await ExistAsync(b => b.BookId == bookId, cancellationToken);
    }
}
