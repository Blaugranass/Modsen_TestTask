using System;
using Library.Application.Pagination;
using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Persistence.Extensions;

public static class BorrowingBookExtension
{
public static async Task<PagedResult<BorrowingBook>> ToPagedAsync(
    this IQueryable<BorrowingBook> query,
    PageParams pageParams,
    CancellationToken cancellationToken = default)
    {
        var count = await query.CountAsync(cancellationToken);
            if(count == 0)
                return new PagedResult<BorrowingBook>([] ,0);

        var page = pageParams.Page ?? 1;
        var pageSize = pageParams.PageSize ?? 10;

        var skip = (page - 1) * pageSize;
        var result = await query.Skip(skip)
                                    .Take(pageSize)
                                    .ToArrayAsync(cancellationToken);

        return new PagedResult<BorrowingBook>(result, count);
    }
}