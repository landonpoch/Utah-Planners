using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using UtahPlanners.Domain.Entity;

namespace UtahPlanners.Infrastructure.Shared
{
    public static class InfrastructureExtensions
    {
        public static IOrderedQueryable<TSource> Sort<TSource, TKey>(this IQueryable<TSource> source,
            Expression<Func<TSource, TKey>> keySelector,
            Direction direction)
        {
            switch (direction)
            {
                case Direction.Descending:
                    return source.OrderByDescending(keySelector);
                case Direction.Ascending:
                default:
                    return source.OrderBy(keySelector);
            }
        }
    }
}
