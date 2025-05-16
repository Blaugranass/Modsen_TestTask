using System;
using Library.Application.Filters;
using Library.Application.Pagination;
using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Persistence.Extensions;

public static class BookExtensions
{
    public static async Task<PagedResult<Book>> ToPagedAsync(
        this IQueryable<Book> query,
        PageParams pageParams,
        CancellationToken cancellationToken = default)
    {
        var count = await query.CountAsync(cancellationToken);
            if(count == 0)
                return new PagedResult<Book>([] ,0);

        var page = pageParams.Page ?? 1;
        var pageSize = pageParams.PageSize ?? 10;

        var skip = (page - 1) * pageSize;
        var result = await query.Skip(skip)
                                    .Take(pageSize)
                                    .ToArrayAsync(cancellationToken);

        return new PagedResult<Book>(result, count);
    }

    public static IQueryable<Book> Filter(this IQueryable<Book> query, BookFilter filter)
    {
        if(string.IsNullOrEmpty(filter.Name))
            query.Where(b => b.Name == filter.Name);

        if(filter.AuthorId is not null)
            query.Where(b => b.AuthorId == filter.AuthorId);
        
        if(filter.GenreId is not null)
            query.Where(b => b.BookGenreId == filter.GenreId);

        return query;
    }
}
