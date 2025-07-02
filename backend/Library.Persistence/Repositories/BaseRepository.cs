using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Library.Application.Interfaces.Repositories;
using Library.Application.Pagination;
using Library.Domain.Entities;
using Library.Persistence.Data;
using Library.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Library.Persistence.Repositories;

public class BaseRepository<T>(LibraryDbContext dbContext) : IRepository<T> where T : BaseEntity
{
    public virtual async Task CreateAsync(T entity,
        CancellationToken cancellationToken = default)
    {
        await dbContext
            .Set<T>()
            .AddAsync(entity, cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public virtual async Task DeleteAsync(Guid id,
        CancellationToken cancellationToken = default)
    {
        await dbContext
            .Set<T>()
            .Where(e => e.Id == id)
            .ExecuteDeleteAsync(cancellationToken);
    }

    public virtual async Task<bool> ExistAsync(Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return await dbContext
            .Set<T>()
            .AnyAsync(predicate, cancellationToken);
    }

    public virtual async Task<T> FirstAsync(Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default,
        params Expression<Func<T, object>>[] includes)
    {
        var query = dbContext
            .Set<T>()
            .AsQueryable();

        if (includes is not null)
        {
            query = includes.Aggregate(query,
                (current, include) => current.Include(include));
        }

        return await query
            .AsNoTracking()
            .FirstAsync(predicate, cancellationToken);
    }

    public virtual async Task<T> GetByIdAsync(Guid id,
        CancellationToken cancellationToken = default,
        params Expression<Func<T, object>>[] includes)
    {
        var query = dbContext
            .Set<T>()
            .AsQueryable();

        if (includes is not null)
        {
            query = includes.Aggregate(query,
                (current, include) => current.Include(include));
        }

        return await query
            .AsNoTracking()
            .FirstAsync(e => e.Id == id, cancellationToken);
    }

    public virtual async Task<IEnumerable<T>> ToListAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext
            .Set<T>()
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public virtual async Task<PagedResult<T>> GetPagedAsync(
        Expression<Func<T, bool>> predicate,
        PageParams pageParams,
        CancellationToken cancellationToken = default,
        params Expression<Func<T, object>>[] includes)
    {
        var query = dbContext
            .Set<T>()
            .AsNoTracking();
            
        if (predicate is not null)
            query = query.Where(predicate);

        if (includes is not null)
        {
            query = includes.Aggregate(query, 
                (current, include) => current.Include(include));
        }

        return await query
                .ToPagedAsync(pageParams, cancellationToken); 
    }
}