using Library.Application.Filters;
using Library.Application.Pagination;
using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Persistence.Extensions;

public static class AuthorExtensions
{
    public static IQueryable<Author> Filter(this IQueryable<Author> query, AuthorFilter filter)
    {
        if(!string.IsNullOrEmpty(filter.LastName))
            query = query.Where(a => a.LastName == filter.LastName);

        return query; 
    }

    public static async Task<PagedResult<Author>> ToPagedAsync(
        this IQueryable<Author> query,
        PageParams pageParams,
        CancellationToken cancellationToken = default)
    {
        var count = await query.CountAsync(cancellationToken);
            if(count == 0)
                return new PagedResult<Author>([] ,0);

        var page = pageParams.Page ?? 1;
        var pageSize = pageParams.PageSize ?? 10;

        var skip = (page - 1) * pageSize;
        var result = await query.Skip(skip)
                                    .Take(pageSize)
                                    .ToArrayAsync(cancellationToken);

        return new PagedResult<Author>(result, count);
    }
}
