using System.Linq;
using Microsoft.EntityFrameworkCore;
using Library.Application.Pagination;

namespace Library.Persistence.Extensions;

public static class QueryableExtensions
{
    public static async Task<PagedResult<T>> ToPagedAsync<T>(
        this IQueryable<T> query,
        PageParams pageParams,
        CancellationToken cancellationToken = default)
    {
        var count = await query.CountAsync(cancellationToken);
        if (count == 0)
            return new PagedResult<T>(Array.Empty<T>(), 0);

        var page = pageParams.Page ?? 1;
        var pageSize = pageParams.PageSize ?? 10;

        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToArrayAsync(cancellationToken);

        return new PagedResult<T>(items, count);
    }
}
