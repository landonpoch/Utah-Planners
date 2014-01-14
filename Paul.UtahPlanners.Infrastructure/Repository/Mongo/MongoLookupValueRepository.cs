using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtahPlanners.Domain.Contract.Repository;
using UtahPlanners.Domain.Entity;

namespace UtahPlanners.Infrastructure.Repository.Mongo
{
    public class MongoLookupValueRepository<T> : ILookupValueRepository<T>
        where T : LookupValue
    {
        private MongoDatabase _mongoDb;
        private MongoCollection<T> _collection;

        private MongoCollection<T> Collection
        {
            get
            {
                if (_collection == null)
                {
                    _collection = _mongoDb.GetCollection<T>(typeof(T).Name);
                }
                return _collection;
            }
        }

        public MongoLookupValueRepository(MongoDatabase mongoDb)
        {
            _mongoDb = mongoDb;
        }

        public T GetLookupValue(Guid id)
        {
            return Collection.FindOneAs<T>(Query<T>.EQ(l => l.Id, id));
        }

        public List<T> GetAllLookupValues()
        {
            return Collection.FindAll()
                .ToList();
        }

        public void AddLookupValue(T lookupValue)
        {
            Collection.Insert(lookupValue);
        }

        public void RemoveLookupValue(Guid id)
        {
            Collection.Remove(Query<T>.EQ(l => l.Id, id));
        }
    }
}
