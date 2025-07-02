using System.Linq.Expressions;
using Library.Application.Filters;
using Library.Application.Pagination;
using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Persistence.Extensions;

public static class AuthorExtensions
{
    public static Expression<Func<Author, bool>> ToExpression(this AuthorFilter filter)
    {
        var predicate = PredicateBuilder.True<Author>();

        if (!string.IsNullOrWhiteSpace(filter.LastName))
            predicate = predicate.And(a => a.LastName == filter.LastName);

        return predicate;
    }
}
