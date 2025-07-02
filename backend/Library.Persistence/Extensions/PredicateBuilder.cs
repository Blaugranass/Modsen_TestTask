using System;
using System.Linq.Expressions;

public static class PredicateBuilder
{
    public static Expression<Func<T, bool>> True<T>() => x => true;

    public static Expression<Func<T, bool>> And<T>(
        this Expression<Func<T, bool>> expr1,
        Expression<Func<T, bool>> expr2)
    {
        var invoked = Expression.Invoke(expr2, expr1.Parameters);
        var body = Expression.AndAlso(expr1.Body, invoked);
        return Expression.Lambda<Func<T, bool>>(body, expr1.Parameters);
    }
}