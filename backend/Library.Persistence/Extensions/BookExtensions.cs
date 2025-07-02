using System;
using System.Linq.Expressions;
using Library.Application.Filters;
using Library.Application.Pagination;
using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Persistence.Extensions;

public static class BookExtensions
{
    public static Expression<Func<Book, bool>> ToExpression(this BookFilter filter)
    {
        var predicate = PredicateBuilder.True<Book>();

        if (!string.IsNullOrWhiteSpace(filter.Name))
            predicate = predicate.And(b => b.Name == filter.Name);

        if (filter.AuthorId is not null)
            predicate = predicate.And(b => b.AuthorId == filter.AuthorId);

        if (filter.GenreId is not null)
            predicate = predicate.And(b => b.AuthorId == filter.AuthorId);

        return predicate;
    }
}
