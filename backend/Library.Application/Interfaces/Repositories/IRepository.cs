using System;
using System.Linq.Expressions;
using Library.Application.Pagination;
using Library.Domain.Entities;

namespace Library.Application.Interfaces.Repositories;

public interface IRepository<T> where T : BaseEntity
{
    Task CreateAsync(T entity,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(Guid id,
        CancellationToken cancellationToken = default);

    Task<T> GetByIdAsync(Guid id,
        CancellationToken cancellationToken = default,
        params Expression<Func<T, object>>[] includes);

    Task<IEnumerable<T>> ToListAsync(CancellationToken cancellationToken = default);

    Task<bool> ExistAsync(Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default);

    Task<T> FirstAsync(Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default,
        params Expression<Func<T, object>>[] includes);

    Task<PagedResult<T>> GetPagedAsync(
        Expression<Func<T, bool>> predicate,
        PageParams pageParams,
        CancellationToken cancellationToken = default,
        params Expression<Func<T, object>>[] includes);
}
