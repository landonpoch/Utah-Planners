using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtahPlanners.Domain.Contract.Persistence;
using UtahPlanners.Domain.Entity;

namespace UtahPlanners.Infrastructure.Repository.Mongo
{
    public class MongoPersistentRepository<T> : IPersistentRepository<T>
        where T : Aggregate
    {
        private MongoCollection<T> _collection;

        public MongoPersistentRepository(MongoCollection<T> collection)
        {
            _collection = collection;
        }

        public T Get(Guid id)
        {
            return _collection.FindOne(Query<T>.EQ(a => a.Id, id));
        }

        public List<T> Find(Func<T, bool> query)
        {
            return _collection.AsQueryable<T>()
                .Where(query)
                .ToList();
        }

        public T Insert(T aggregate)
        {
            _collection.Insert(aggregate);
            return aggregate;
        }

        public bool Update(Guid id, T aggregate)
        {
            typeof(Aggregate).GetProperty("Id")
                .SetValue(aggregate, id);
            
            var result = _collection.Save(aggregate);
            return result.Ok;
        }

        public bool Remove(Guid id)
        {
            var result = _collection.Remove(Query<T>.EQ(a => a.Id, id));
            return result.Ok;
        }
    }
}
