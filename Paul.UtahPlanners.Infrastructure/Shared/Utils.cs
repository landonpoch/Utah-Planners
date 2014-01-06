using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using MongoDB.Driver;
using UtahPlanners.Infrastructure.DAO;
using UtahPlanners.Domain.DTO;
using UtahPlanners.Domain.Entity;

namespace UtahPlanners.Infrastructure.Shared
{
    public static class Extensions
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

    public static class Utils
    {
        public static string Convert<T>(PropertyContext context, Guid? key, Func<T, string> getDescription)
            where T : class
        {
            var item = context.Set<T>()
                .Find(key);
            return getDescription(item);
        }

        public static string Convert<T>(MongoDatabase db, Guid? key, Func<T, string> getDescription)
            where T : class
        {
            var item = db.GetCollection<T>(typeof(T).Name)
                .FindOneById(key);
            return getDescription(item);
        }

        public static string GetDescription(CommonAreas code) { return code.Description; }
        public static string GetDescription(BuildingEnclosure code) { return code.Description; }
        public static string GetDescription(NeighborhoodCondition code) { return code.Description; }
        public static string GetDescription(SocioEcon code) { return code.Description; }
        public static string GetDescription(StreetConnectivity code) { return code.Description; }
        public static string GetDescription(StreetSafety code) { return code.Description; }
        public static string GetDescription(StreetType code) { return code.Description; }
        public static string GetDescription(StreetWalkability code) { return code.Description; }
        public static string GetDescription(PropertyType code) { return code.Description; }
    }
}
