using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using UtahPlanners.Domain.Entity;
using MongoDB.Driver;
using UtahPlanners.Infrastructure.DAO;

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
        public static string Convert<T>(PropertiesDB context, int? key, Func<T, string> getDescription)
            where T : class
        {
            var item = context.Set<T>()
                .Find(key);
            return getDescription(item);
        }

        public static string Convert<T>(MongoDatabase db, int? key, Func<T, string> getDescription)
            where T : class
        {
            var item = db.GetCollection<T>(typeof(T).Name)
                .FindOneById(key);
            return getDescription(item);
        }

        public static string GetDescription(CommonCode code) { return code.description; }
        public static string GetDescription(EnclosureCode code) { return code.description; }
        public static string GetDescription(NeighborhoodCode code) { return code.description; }
        public static string GetDescription(SocioEconCode code) { return code.description; }
        public static string GetDescription(StreetconnCode code) { return code.description; }
        public static string GetDescription(StreetSafteyCode code) { return code.description; }
        public static string GetDescription(StreetType code) { return code.description; }
        public static string GetDescription(StreetwalkCode code) { return code.description; }
        public static string GetDescription(PropertyType code) { return code.description; }
    }
}
